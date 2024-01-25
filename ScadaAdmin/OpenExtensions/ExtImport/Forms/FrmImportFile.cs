﻿using Scada.Admin.Extensions.ExtImport.Code;
using Scada.Admin.Project;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvModbus.Config;
using Scada.Comm.Drivers.DrvModbus.Protocol;
using Scada.Data.Entities;
using System.Text.RegularExpressions;
using System.Xml;
using Scada.Forms;

namespace Scada.Admin.Extensions.ExtImport.Forms
{
    public partial class FrmImportFile : Form
    {
        public Dictionary<string, List<string>> importedRows;
        public List<Cnl> importedChannels;
        public List<Cnl> conflictualChannels;
        public List<Cnl> channelsToCreateAfterMerge;
        public string selectedFileName;
        private Device selectedDevice;
        private bool conflictsAreSolved = false;
        Dictionary<string, int> availablePrefixSuffix = new Dictionary<string, int> { { "TagCode", -1 }, { "Mnémonique", 0 }, { "Format", 1 }, { "Comment", 2 } };
        string selectedPrefix;
        string selectedSuffix;
        //splittedChannels contains the channels that are waitinf for a formula, and keys are parents channels
        Dictionary<string, List<Cnl>> splittedChannels; 

        private readonly ScadaProject project;
        public FrmImportFile(ScadaProject prj)
        {
            InitializeComponent();
            project = prj;
            cbBoxPrefix.Enabled = false;
            cbBoxSuffix.Enabled = false;
            selectedFileName = "";
            panel2.Visible = false;
            panel3.Visible = false;
            importedRows = new Dictionary<string, List<string>>();
            splittedChannels = new Dictionary<string, List<Cnl>>();
            button1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            cbDevice.Items.Clear();
            project.ConfigDatabase.DeviceTable.AsEnumerable().ToList().ForEach(d => cbDevice.Items.Add(d.Name));
            cbBoxPrefix.Items.Add("None");
            cbBoxPrefix.Items.AddRange(availablePrefixSuffix.Keys.ToArray());
            cbBoxPrefix.SelectedItem = "None";
            cbBoxSuffix.Items.Add("None");
            cbBoxSuffix.Items.AddRange(availablePrefixSuffix.Keys.ToArray());
            cbBoxSuffix.SelectedItem = "None";

            LoadExtImportConfig();
        }
        /// <summary>
        /// Reads extImport config file and loads its content
        /// </summary>
        private void LoadExtImportConfig()
        {
            string extImportConfigFolder = string.Format("{0}\\Instances\\Default\\ExtImport", project.ProjectDir);
            string extImportConfigPath = string.Format("{0}\\extImportConfig.xml", extImportConfigFolder);
            if (!Directory.Exists(extImportConfigFolder))
            {
                Directory.CreateDirectory(extImportConfigFolder);
            }
            if (File.Exists(extImportConfigPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(extImportConfigPath);
                XmlNode rootNode = xmlDoc.SelectSingleNode("extImportConfig");
                XmlNode default2ByteOrderNode = rootNode.SelectSingleNode("default2ByteOrder");
                if (default2ByteOrderNode != null)
                {
                    textBox1.Text = default2ByteOrderNode.InnerText;
                }
                XmlNode default4ByteOrderNode = rootNode.SelectSingleNode("default4ByteOrder");
                if (default4ByteOrderNode != null)
                {
                    textBox2.Text = default4ByteOrderNode.InnerText;
                }
                XmlNode default8ByteOrderNode = rootNode.SelectSingleNode("default8ByteOrder");
                if (default8ByteOrderNode != null)
                {
                    textBox3.Text = default8ByteOrderNode.InnerText;
                }
            }
            else
            {
                SaveExtImportConfig(extImportConfigPath);
            }
        }

        private void SaveExtImportConfig(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("extImportConfig");

            XmlNode default2ByteOrderNode = xmlDoc.CreateElement("default2ByteOrder");
            default2ByteOrderNode.InnerText = textBox1.Text;
            rootNode.AppendChild(default2ByteOrderNode);

            XmlNode default4ByteOrderNode = xmlDoc.CreateElement("default4ByteOrder");
            default4ByteOrderNode.InnerText = textBox2.Text;
            rootNode.AppendChild(default4ByteOrderNode);

            XmlNode default8ByteOrderNode = xmlDoc.CreateElement("default8ByteOrder");
            default8ByteOrderNode.InnerText = textBox3.Text;
            rootNode.AppendChild(default8ByteOrderNode);

            xmlDoc.AppendChild(rootNode);
            xmlDoc.Save(path);
        }

        /// <summary>
        /// Action triggered on click on "select file" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Fichiers SCY (*.scy)|*.scy";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
                txtPathFile.Text = selectedFileName;
                if (selectedFileName != "")
                {
                    readFile(selectedFileName);
                    conflictsAreSolved = false;
                    refreshFormAccesses();
                }
            }
        }

        /// <summary>
        /// read the file and fill the importedRows dictionary
        /// </summary>
        /// <param name="fileName"></param>
        private void readFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                if (Path.GetExtension(fileName) == ".txt" || Path.GetExtension(fileName) == ".TXT")
                {
                    bool isFirstLine = true;
                    bool isPL7 = false;
                    importedRows.Clear();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (isFirstLine)
                        {
                            if (line.StartsWith("%"))
                            {
                                isPL7 = true;
                            }
                            isFirstLine = false;
                        }

                        readAutomateLine(line, isPL7);
                    }
                }

                else if (Path.GetExtension(fileName) == ".SCY")
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        readSCYPL7(line);
                    }
                }

                else
                {
                    selectedFileName = "";
                }
            }
        }

        /// <summary>
        /// Create a channel object, filled with data from a row of the imported file
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="rowValue"></param>
        /// <returns></returns>
        private Cnl generateChannelFromRow(KeyValuePair<string, List<string>> row)
        {
            List<Cnl> bitChannels = new List<Cnl>();
            Cnl cnl = new Cnl();
            cnl.Name = row.Value[0];
            cnl.TagCode = row.Key;
            if (ConfigDictionaries.CnlDataType.ContainsKey(row.Value[1]))
            {
                cnl.DataTypeID = ConfigDictionaries.CnlDataType[row.Value[1]];
            }
            else if (row.Value[1].Contains("ARRAY"))
            {
                if (ConfigDictionaries.CnlDataType.ContainsKey(row.Value[1].Split(' ')[2]))
                {
                    cnl.DataTypeID = ConfigDictionaries.CnlDataType[row.Value[1].Split(' ')[2]];
                }
                int arrayLength = int.Parse(row.Value[1].Split(' ')[0].Split("..")[1].Split(']')[0])+1;
                for(int i=0; i < arrayLength; i++)
                {
                    Cnl cnlArray = new Cnl();
                    cnlArray.Name = string.Format("{0}[{1}]", row.Value[0], i);
                    cnlArray.TagCode = string.Format("{0}.{1}", row.Key, i);
                    if (ConfigDictionaries.CnlDataType.ContainsKey(row.Value[1].Split(' ')[2]))
                    {
                        cnlArray.DataTypeID = ConfigDictionaries.CnlDataType[row.Value[1].Split(' ')[2]];
                    }
                    if(!splittedChannels.ContainsKey(cnl.TagCode))
                    {
                        splittedChannels.Add(cnl.TagCode, new List<Cnl>());
                    }
                    splittedChannels[cnl.TagCode].Add(cnlArray);
                }
            }
            //default Type is input/output
            cnl.CnlTypeID = 2;
            cnl.DeviceNum = selectedDevice.DeviceNum;


            return cnl;
        }

        /// <summary>
        /// Hides or shows elements in the form, considering different verifications.
        /// </summary>
        private void refreshFormAccesses()
        {
            //check if device is selected and if file is chosen
            bool areFileAndDeviceSelected = selectedFileName != "" && selectedDevice != null;

            //enable controls if file and device are selected, disbale otherwise
            cbBoxPrefix.Enabled = areFileAndDeviceSelected;
            cbBoxSuffix.Enabled = areFileAndDeviceSelected;
            button1.Enabled = areFileAndDeviceSelected;
            textBox1.Enabled = areFileAndDeviceSelected;
            textBox2.Enabled = areFileAndDeviceSelected;
            textBox3.Enabled = areFileAndDeviceSelected;

            if (areFileAndDeviceSelected)
            {
                splittedChannels =  new Dictionary<string, List<Cnl>>();
                //update generated channels list
                importedChannels = new List<Cnl>();

                importedChannels = importedRows.Select(generateChannelFromRow).ToList();
                //detect conflicts
                DetectConflicts();
                //We consider that there is a conflict if there are conflictual channels and if user has not already solved conflicts
                bool hasConflicts = conflictualChannels.Count() > 0 && !conflictsAreSolved;
                if (!hasConflicts && !conflictsAreSolved)
                {
                    channelsToCreateAfterMerge = importedChannels.DeepClone();
                }
                //hide and display conflicts related labels and button container
                panel2.Visible = hasConflicts;
                panel3.Visible = !hasConflicts;
                button1.Enabled = !hasConflicts;
            }
        }

        /// <summary>
        /// Find channels that enter in conflict with imported file rows
        /// </summary>
        private void DetectConflicts()
        {
            conflictualChannels = project.ConfigDatabase.CnlTable.Where(channel => importedChannels.Any(c => c.TagCode == channel.TagCode)).ToList();
        }


        /// <summary>
        /// Read a line from an automate file
        /// </summary>
        /// <param name="l"></param>
        private void readAutomateLine(string line, bool isPL7)
        {
            int adressIndex = isPL7 ? 0 : 1;
            int mnemoniqueIndex = isPL7 ? 1 : 0;
            int typeIndex = 2;
            int commentIndex = 3;

            string[] columns = line.Split('\t');
            if (columns[adressIndex] == "")
            {
                return;
            }

            string adress = new string(columns[adressIndex].SkipWhile(x => !char.IsDigit(x)).ToArray());
            if (importedRows.ContainsKey(adress))
            {
                return;
            }

            string prefix = Regex.Split(columns[adressIndex], @"[0-9]").First();

            this.importedRows.Add(adress, new List<string>
            {
                columns[mnemoniqueIndex],
                columns[typeIndex],
                columns[commentIndex].Replace("\"", ""),
                prefix
            });
        }

        /// <summary>
        /// Read a line from a SCY file
        /// </summary>
        /// <param name="l"></param>
        private void readSCYPL7(string l)
        {
            string[] splitInTwo = l.Split(" AT ");

            if (splitInTwo.Length >= 2)
            {
                string mnemonique = splitInTwo[0];

                string[] splitAdress = splitInTwo[1].Split(" : ");
                string adress = splitAdress[0];

                string[] splitType = splitAdress[1].Split(" (*");
                string type = splitType[0];

                string[] splitComment = splitType[1].Split('*');
                string comment = splitComment[0].Replace("\"", "");

                string prefix = Regex.Split(adress, @"[0-9]").First();

                List<string> list = new List<string>
                {
                    mnemonique,
                    type,
                    comment,
                    prefix,
                };

                importedRows.Add(adress, list);
            }
        }

        /// <summary>
        /// We add created channels to the project
        /// </summary>
        /// <param name="cnls"></param>
        private void AddChannelsToProject(List<Cnl> cnls)
        {
            foreach (var cnl in cnls)
            {
                //if cnl.cnlnum is not defined, take the next one in the project's channels list
                if (cnl.CnlNum == 0)
                {
                    cnl.CnlNum = project.ConfigDatabase.CnlTable.Count() > 0 ? project.ConfigDatabase.CnlTable.OrderBy(cnl => cnl.CnlNum).Last().CnlNum + 1 : 1;
                }
                project.ConfigDatabase.CnlTable.AddItem(cnl);
                if(splittedChannels != null && splittedChannels.ContainsKey(cnl.TagCode))
                {
                    foreach(Cnl child in splittedChannels[cnl.TagCode])
                    {
                        child.FormulaEnabled = true;
                        int bitIndex = splittedChannels[cnl.TagCode].FindIndex(c => c.TagCode == child.TagCode);
                        child.InFormula = $"Getbit(Val({cnl.CnlNum}),{bitIndex})";
                        if(child.CnlNum == 0)
                            child.CnlNum = project.ConfigDatabase.CnlTable.Count() > 0 ? project.ConfigDatabase.CnlTable.OrderBy(cnl => cnl.CnlNum).Last().CnlNum + 1 : 1;
                        //input/output by default
                        child.CnlTypeID = 2;
                        child.DeviceNum = selectedDevice.DeviceNum;
                        project.ConfigDatabase.CnlTable.AddItem(child);
                    }
                }
            }
            project.ConfigDatabase.CnlTable.Modified = true;
        }

        /// <summary>
        /// Action triggered on click on "resolve conflicts" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            conflictsAreSolved = false;

            //We open the merge form
            FrmCnlsMerge frmCnlsMerge = new(project, conflictualChannels, importedChannels);

            //If the merge form is closed with OK, we update the channels to create
            if (frmCnlsMerge.ShowDialog() == DialogResult.OK)
            {
                channelsToCreateAfterMerge = frmCnlsMerge.channelsToCreate;
                conflictsAreSolved = true;

                //We refresh the display of the form
                refreshFormAccesses();
            }
        }

        /// <summary>
        /// On click on OK button, create channels in project according to merge form result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            //Then, we generate a device configuration according to imported rows
            bool doNext  = ImportDeviceConfiguration();

            if (doNext)
            {
                //save the default byte order in project config
                string extImportConfigPath = string.Format("{0}\\Instances\\Default\\ExtImport\\extImportConfig.xml", project.ProjectDir);
                SaveExtImportConfig(extImportConfigPath);

                //Finally, we close the form
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// On click on Cancel button, close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Action triggered on device selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ScadaException"></exception>
        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDevice = project.ConfigDatabase.DeviceTable.ToList()[cbDevice.SelectedIndex];
            if (channelsToCreateAfterMerge != null)
            {
                channelsToCreateAfterMerge = channelsToCreateAfterMerge.Select(c => { c.DeviceNum = selectedDevice.DeviceNum; return c; }).ToList();
            }
            refreshFormAccesses();
        }

        /// <summary>
        /// Generates the XML configuration file for the device
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private XmlDocument GenerateXmlConfigurationFile(DeviceTemplate template)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDecl);
            XmlElement rootElem = xmlDoc.CreateElement("DeviceTemplate");
            xmlDoc.AppendChild(rootElem);
            DeviceTemplateOptions options = new DeviceTemplateOptions();
            options.SaveToXml(rootElem.AppendElem("Options"));
            XmlElement elemGroupsElem = rootElem.AppendElem("ElemGroups");
            foreach (ElemGroupConfig elemGroupConfig in template.ElemGroups)
            {
                elemGroupConfig.SaveToXml(elemGroupsElem.AppendElem("ElemGroup"));
            }
            return xmlDoc;
        }

        /// <summary>
        /// Creates and replaces the device configuration according to imported rows
        /// </summary>
        private bool ImportDeviceConfiguration()
        {
            bool doNext = false;
            DeviceTemplate template = GenerateDeviceTemplate();
            XmlDocument xmlDoc = GenerateXmlConfigurationFile(template);

            //find current device configuration file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = string.Format("{0}.xml", selectedDevice.Name);

            DeviceConfig currentConfig = null;
            ProjectInstance currentInstance = null;

            foreach (ProjectInstance instance in project.Instances)
            {
                if (instance.LoadAppConfig(out _) && instance.CommApp.Enabled)
                {
                    foreach (LineConfig lineConfig in instance.CommApp.AppConfig.Lines)
                    {
                        foreach (DeviceConfig deviceConfig in lineConfig.DevicePolling)
                        {
                            if (deviceConfig.DeviceNum == selectedDevice.DeviceNum)
                            {
                                currentInstance = instance;
                                currentConfig = deviceConfig;
                                saveFileDialog1.FileName = deviceConfig.PollingOptions.CmdLine;
                                continue;
                            }
                        }
                    }
                }
            }

            saveFileDialog1.InitialDirectory = string.Format("{0}\\Instances\\Default\\ScadaComm\\Config", this.project.ProjectDir);
            saveFileDialog1.Filter = "Fichiers XML (*.xml)|*.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //First, we add channels chosen in merge form to the project's channels
                AddChannelsToProject(channelsToCreateAfterMerge);

                //If not all imported rows were in conflict, we create channels for all other rows and add them to the project's channels
                //Warning : there can be multiple conflictual channels for one imported channel
                if (importedChannels.Count() != importedChannels.Where(ic => conflictualChannels.Any(c => c.TagCode == ic.TagCode)).Count() && conflictualChannels.Count() > 0)
                {
                    // we create channels for all other rows, which were not in conflict
                    List<Cnl> nonConflictualChannels = importedChannels.Where(ic => !conflictualChannels.Any(c => c.TagCode == ic.TagCode)).ToList();
                    //we add them to the project's channels
                    AddChannelsToProject(nonConflictualChannels);
                }
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    xmlDoc.Save(sw);
                }

                //replace device configuration file in project
                var newFilename = saveFileDialog1.FileName.Split('\\').Last();
                if (currentConfig != null)
                {
                    currentConfig.PollingOptions.CmdLine = newFilename;
                    string fileName = Path.Combine(currentInstance.CommApp.ConfigDir, CommConfig.DefaultFileName);
                    if (!currentInstance.CommApp.AppConfig.Save(string.Format(fileName, this.project.ProjectDir), out string errMess))
                    {
                        ScadaUiUtils.ShowError(errMess);
                    }
                }
                doNext = true;
            }
            return doNext;
        }

        /// <summary>
        /// Generates a device template configuration according to imported rows
        /// </summary>
        /// <returns></returns>
        private DeviceTemplate GenerateDeviceTemplate()
        {
            //Initializations
            DeviceTemplate template = new DeviceTemplate();
            ElemGroupConfig newElemenGroup = new ElemGroupConfig();
            Dictionary<int, string> dataTypes = project.ConfigDatabase.DataTypeTable.ToDictionary(x => x.DataTypeID, x => x.Name);
            string previousPrefix = "";
            ElemType previousType = ElemType.Undefined;
            Dictionary<string, ElemType> elemTypeDico = ConfigDictionaries.ElemTypeDictionary;
            Dictionary<string, int> cnlDataType = ConfigDictionaries.CnlDataType;

            //for each imported row
            foreach (KeyValuePair<string, List<string>> row in importedRows)
            {
                var prefix = row.Value[3] ?? "";
                int rowIndex = importedRows.Keys.ToList().IndexOf(row.Key);
                //if row tagcode contains a non-digit character (except prefix)
                if (!row.Key.All(char.IsDigit) && rowIndex > 0)
                {
                    continue;
                }


                //we create a new configuration element
                ElemConfig newElem = new ElemConfig();
                //we set its properties according to imported row
                string newType = elemTypeDico.Keys.Contains(row.Value[1]) ? row.Value[1] : cnlDataType.FirstOrDefault(t => t.Value == dataTypes.FirstOrDefault(dt => dt.Value == row.Value[1]).Key).Key;
                newElem.ElemType = elemTypeDico.Keys.Contains(row.Value[1]) ? elemTypeDico[row.Value[1]] : ElemType.Undefined;

                switch (ModbusUtils.GetDataLength(newElem.ElemType))
                {
                    case 2:
                        newElem.ByteOrder = textBox1.Text;
                        break;
                    case 4:
                        newElem.ByteOrder = textBox2.Text;
                        break;
                    case 8:
                        newElem.ByteOrder = textBox3.Text;
                        break;
                    default:
                        newElem.ByteOrder = "0123";
                        break;
                }

                newElem.Name = row.Value[0];
                newElem.TagCode = row.Key;
                newElemenGroup.DataBlock = DataBlock.HoldingRegisters;
                int index = importedRows.Keys.ToList().IndexOf(row.Key);

                //if these conditions are met, we add the current element group to the template and create a new one
                if (index == 0 || prefix != previousPrefix || newElem.ElemType != previousType || (prefix == "%MW" && newElemenGroup.Elems.Count == 125) || (prefix == "%M" && newElemenGroup.Elems.Count == 2000))
                {
                    if (index > 0)
                    {
                        template.ElemGroups.Add(newElemenGroup);
                    }
                    newElemenGroup = new ElemGroupConfig();
                    newElemenGroup.DataBlock = DataBlock.HoldingRegisters;
                    newElemenGroup.Address = int.Parse(Regex.Replace(row.Key, @"[^0-9]", ""));
                }
                previousPrefix = prefix;
                previousType = newElem.ElemType;

                //we add the new element to the current element group
                newElemenGroup.Elems.Add(newElem);

                if (row.Value[1].Contains("ARRAY"))
                {
                    //here, we assume that row.value[1] is like "ARRAY[0..5] OF BOOL"
                    try
                    {
                        string[] arrayType = row.Value[1].Split(' ');
                        newElem.ElemType = elemTypeDico.Keys.Contains(arrayType[2]) ? elemTypeDico[arrayType[2]] : ElemType.Undefined;
                        int arrayLength = int.Parse(arrayType[0].Split("..")[1].Split(']')[0])+1;
                        for (int i = 0; i < arrayLength; i++)
                        {
                            ElemConfig newElemArray = new ElemConfig();
                            newElemArray.ElemType = newElem.ElemType;
                            switch (ModbusUtils.GetDataLength(newElem.ElemType))
                            {
                                case 2:
                                    newElemArray.ByteOrder = textBox1.Text;
                                    break;
                                case 4:
                                    newElemArray.ByteOrder = textBox2.Text;
                                    break;
                                case 8:
                                    newElemArray.ByteOrder = textBox3.Text;
                                    break;
                                default:
                                    newElemArray.ByteOrder = "0123";
                                    break;
                            }
                            newElemArray.Name = string.Format("{0}[{1}]", row.Value[0], i);
                            newElemArray.TagCode = string.Format("{0}.{1}", row.Key, i);
                            newElemenGroup.Elems.Add(newElemArray);

                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error reading the file: one of the variables is an ARRAY but does not appear to be in the form 'ARRAY[0..X] OF TYPE'. Details: " + e.Message);
                    }
                }
            }
            template.ElemGroups.Add(newElemenGroup);
            return template;
        }

        /// <summary>
        /// Action triggered on prefix selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ScadaException"></exception>
        private void cbBoxPrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (importedRows.Count() == 0)
            {
                return;
            }
            string previousPrefix = selectedPrefix;
            selectedPrefix = cbBoxPrefix.SelectedIndex > 0 ? availablePrefixSuffix.Keys.ElementAt(cbBoxPrefix.SelectedIndex - 1) : null;
            int? selectedPrefixRowColumnIndex = selectedPrefix != null ? availablePrefixSuffix.Values.ElementAt(cbBoxPrefix.SelectedIndex - 1) : null;
            int? selectedSuffixRowColumnIndex = cbBoxSuffix.SelectedIndex > 0 ? availablePrefixSuffix.Values.ElementAt(cbBoxSuffix.SelectedIndex - 1) : null;

            string NameWithoutPrefix(int rowIndex)
            {
                return selectedSuffixRowColumnIndex != null
                    ? selectedSuffixRowColumnIndex >= 0
                        ? string.Join(" - ", importedRows.Values.ElementAt(rowIndex)[0], importedRows.Values.ElementAt(rowIndex)[selectedSuffixRowColumnIndex ?? 0])
                        : string.Join(" - ", importedRows.Values.ElementAt(rowIndex)[0], importedRows.Keys.ElementAt(rowIndex))
                    : importedRows.Values.ElementAt(rowIndex)[0];
            }

            Func<Cnl, int, Cnl> updateChannel = (c, i) =>
            {
                c.Name = selectedPrefixRowColumnIndex != null ?

                string.Format("{0} - {1}", selectedPrefixRowColumnIndex >= 0
                    ? importedRows.Values.ElementAt(i)[selectedPrefixRowColumnIndex ?? 0]
                    : importedRows.Keys.ElementAt(i), NameWithoutPrefix(i))

                :
                NameWithoutPrefix(i);
                return c;
            };

            importedChannels = importedChannels.Select(updateChannel).ToList();
            channelsToCreateAfterMerge = channelsToCreateAfterMerge != null ? channelsToCreateAfterMerge.Select(updateChannel).ToList() : importedChannels.DeepClone();
        }



        /// <summary>
        /// Action triggered on suffix selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ScadaException"></exception>
        private void cbBoxSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (importedRows.Count() == 0)
            {
                return;
            }
            string previousSuffix = selectedSuffix;
            selectedSuffix = cbBoxSuffix.SelectedIndex > 0 ? availablePrefixSuffix.Keys.ElementAt(cbBoxSuffix.SelectedIndex - 1) : null;
            int? selectedPrefixRowColumnIndex = selectedPrefix != null ? availablePrefixSuffix.Values.ElementAt(cbBoxPrefix.SelectedIndex - 1) : null;
            int? selectedSuffixRowColumnIndex = cbBoxSuffix.SelectedIndex > 0 ? availablePrefixSuffix.Values.ElementAt(cbBoxSuffix.SelectedIndex - 1) : null;

            string NameWithoutSuffix(int rowIndex)
            {
                return selectedPrefixRowColumnIndex != null
                    ? selectedPrefixRowColumnIndex >= 0
                        ? string.Join(" - ", importedRows.Values.ElementAt(rowIndex)[selectedPrefixRowColumnIndex ?? 0], importedRows.Values.ElementAt(rowIndex)[0])
                        : string.Join(" - ", importedRows.Keys.ElementAt(rowIndex), importedRows.Values.ElementAt(rowIndex)[0])
                    : importedRows.Values.ElementAt(rowIndex)[0];
            }

            Func<Cnl, int, Cnl> updateChannel = (c, i) =>
            {
                c.Name = selectedSuffixRowColumnIndex != null ?
                c.Name = string.Format("{0} - {1}",
                    NameWithoutSuffix(i),
                    selectedSuffixRowColumnIndex >= 0 ? importedRows.Values.ElementAt(i)[selectedSuffixRowColumnIndex ?? 0]
                    : importedRows.Keys.ElementAt(i))
                : NameWithoutSuffix(i);
                return c;
            };

            importedChannels = importedChannels.Select(updateChannel).ToList();
            channelsToCreateAfterMerge = channelsToCreateAfterMerge != null ? channelsToCreateAfterMerge.Select(updateChannel).ToList() : importedChannels.DeepClone();
        }
    }
}
