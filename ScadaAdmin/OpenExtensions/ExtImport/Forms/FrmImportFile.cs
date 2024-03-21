using Scada.Admin.Extensions.ExtImport.Code;
using Scada.Admin.Project;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvModbus.Config;
using Scada.Comm.Drivers.DrvModbus.Protocol;
using Scada.Data.Entities;
using System.Text.RegularExpressions;
using System.Xml;
using Scada.Forms;
using System.Data;

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
        Dictionary<string, List<string>> ghostRows;
        Dictionary<string, string> ghostChildren;
        Dictionary<string, List<string>> ghostArrayElementRow;

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
            ghostRows = new Dictionary<string, List<string>>();
            ghostChildren = new Dictionary<string, string>();
            ghostArrayElementRow = new Dictionary<string, List<string>>();
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

        private Dictionary<string, List<string>> getGhostsRows()
        {
            Dictionary<string, ElemType> elemTypeDico = ConfigDictionaries.ElemTypeDictionary;
            Dictionary<string, List<string>> ghostRows = new Dictionary<string, List<string>>();
            //for each imported row
            for (int i = 0; i < importedRows.Count(); i++)
            {
                var row = importedRows.ElementAt(i);
                var prefix = row.Value[3] ?? "";
                int rowIndex = importedRows.Keys.ToList().IndexOf(row.Key);
                //If row tagcode contains a non-digit character (except prefix)
                if (!row.Key.All(char.IsDigit))// && rowIndex > 0)
                {
                    //here, we are facing a row that is not a variable, but a bit, because its key is like "X.Y" and not just "X".
                    //in this case, we have to create a variable and a channel for the "parent" value, supposed to have key "X".
                    //we define X
                    string leftPart = Regex.Split(row.Key, @"[^0-9]")[0];

                    //if no other row has key "X", it means we are watching the first row of a list of bits. 
                    //we have to define the length of the list, to know what type to choose for the variable with key "X"
                    if (!ghostRows.Keys.Contains(leftPart))
                    {
                        int bitListLength = 0;
                        string rLeftPart = leftPart;
                        while (rLeftPart == leftPart && (rowIndex + bitListLength - 1) < importedRows.Count() - 1)
                        {
                            bitListLength++;
                            KeyValuePair<string, List<string>> r = importedRows.ElementAt(rowIndex + bitListLength - 1);
                            rLeftPart = Regex.Split(r.Key, @"[^0-9]")[0];
                        }

                        //now we know the length of the bit list, we can deduce the type of the variable with key "X"
                        string parentRowType = "";
                        foreach (KeyValuePair<string, ElemType> type in elemTypeDico)
                        {
                            if (ModbusUtils.GetDataLength(type.Value) == bitListLength / 8)
                            {
                                parentRowType = type.Key;
                                break;
                            }
                        }

                        //from here, we know the type of the variable with key "X", so we can create it
                        //importedRows.Add(leftPart, new List<string> { row.Value[0], parentRowType, row.Value[2], row.Value[3] });

                        //we add the tagcode of the current row to the splittedChannels list, to read it later
                        ghostRows.Add(leftPart, new List<string> { row.Value[0], parentRowType, row.Value[2], row.Value[3] });
                        //continue;
                    }
                }
            }
            return ghostRows;
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
                        //ignore non conform lines
                        if (line.Split('\t').Length < 4 || (line.Split('\t').Where(x => x != "").Count() < 3))
                        {
                            continue;
                        }
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
            panel5.Visible = areFileAndDeviceSelected;


            if (areFileAndDeviceSelected)
            {
                ghostArrayElementRow = new Dictionary<string, List<string>>();
                splittedChannels = new Dictionary<string, List<Cnl>>();
                //update generated channels list
                importedChannels = new List<Cnl>();
                ghostRows = getGhostsRows();

                foreach (KeyValuePair<string, List<string>> ghostRow in ghostRows)
                {
                    if (!importedRows.ContainsKey(ghostRow.Key))
                    {
                        ghostRow.Value[0] = ghostRow.Value[0].Substring(0, ghostRow.Value[0].Length - 1);
                        ghostRow.Value[2] = ghostRow.Value[2].Substring(0, ghostRow.Value[2].Length - 1);
                        importedRows.Add(ghostRow.Key, ghostRow.Value);
                    }
                }
                importedChannels = GenerateChannelFrowRow(importedRows);
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
        /// Create a channel object, filled with data from a row of the imported file
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="rowValue"></param>
        /// <returns></returns>

        private List<Cnl> GenerateChannelFrowRow(Dictionary<string, List<string>> dico)
        {
            List<Cnl> list = new List<Cnl>();
            Dictionary<string, ElemType> elemTypeDico = ConfigDictionaries.ElemTypeDictionary;

            foreach (KeyValuePair<string, List<string>> row in importedRows)
            {
                if (row.Value[1].Contains("ARRAY"))
                {
                    try
                    {
                        string[] arrayType = row.Value[1].Split(' ');
                        var elemType = elemTypeDico.Keys.Contains(arrayType[2]) ? elemTypeDico[arrayType[2]] : ElemType.Undefined;
                        int arrayLength = int.Parse(arrayType[0].Split("..")[1].Split(']')[0]) + 1;
                        decimal dataLength = ModbusUtils.GetDataLength(elemType);

                        for (int j = 0; j < arrayLength; j++)
                        {
                            Cnl cnlArray = new Cnl();
                            cnlArray.Name = string.Format("{0}_{1}", row.Value[0], j);
                            cnlArray.TagCode = string.Format("{0}", decimal.Parse(row.Key) + (dataLength == 1 ? dataLength * j : Math.Ceiling(dataLength * j / 2)));
                            cnlArray.CnlTypeID = 2;
                            cnlArray.DeviceNum = selectedDevice.DeviceNum;
                            cnlArray.Active = true;
                            list.Add(cnlArray);
                            ghostArrayElementRow.Add(cnlArray.TagCode, new List<string> { cnlArray.Name, arrayType[2], row.Value[2], row.Value[3] });
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error reading the file: one of the variables is an ARRAY but does not appear to be in the form 'ARRAY[0..X] OF TYPE'. Details: " + e.Message);
                    }
                }
                else
                {
                    Cnl cnl = new Cnl();
                    cnl.Name = row.Value[0];

                    //default Type is input/output
                    cnl.CnlTypeID = 2;
                    cnl.DeviceNum = selectedDevice.DeviceNum;
                    cnl.Active = true;

                    //if it is a children of ghost row
                    if (ghostRows.ContainsKey(row.Key.Split('.')[0]) && !ghostRows.ContainsKey(row.Key))
                    {
                        cnl.FormulaEnabled = true;
                        if (!ghostChildren.ContainsKey(cnl.Name))
                            ghostChildren.Add(cnl.Name, row.Key.Split('.')[0]);

                        //set type to "calculated"
                        cnl.CnlTypeID = 3;
                        cnl.TagCode = row.Key;
                    }
                    else
                    {
                        cnl.TagCode = row.Key;
                        if (ghostRows.ContainsKey(row.Key))
                        {
                            cnl.FormatID = 2;
                        }
                    }

                    list.Add(cnl);
                }
            }


            return list;
        }

        /// <summary>
        /// Find channels that enter in conflict with imported file rows
        /// </summary>
        private void DetectConflicts()
        {

            conflictualChannels = project.ConfigDatabase.CnlTable.Where(channel => importedChannels.Any(c => c.TagCode == channel.TagCode && channel.DeviceNum == selectedDevice.DeviceNum)).ToList();
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
                if (columns[typeIndex].Contains("ARRAY"))
                {
                    return;
                }
                else if (importedRows[adress][1].Contains("ARRAY"))
                {
                    importedRows.Remove(adress);
                }
                else
                {
                    MessageBox.Show("Error reading the file: the tagcode " + adress + " is already used in the file.");
                }
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
            }
            int count = 0;
            foreach (Cnl cnl in project.ConfigDatabase.CnlTable)
            {
                //add formula
                if (cnl.FormulaEnabled && ghostChildren.ContainsKey(cnl.Name))
                {
                    Cnl parent = cnls.FirstOrDefault(t => t.TagCode == ghostChildren[cnl.Name] && !t.FormulaEnabled);
                    if (parent != null)
                    {
                        cnl.InFormula = $"GetBit(Val({parent.CnlNum}),{count})";
                    }
                    count++;
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
            bool doNext = ImportDeviceConfiguration();

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
            bool previousWasArray = false;
            decimal previousDataLength = 0;
            ElemConfig previousElem = new ElemConfig();

            //for each imported row
            foreach (KeyValuePair<string, List<string>> row in importedRows)
            {
                var prefix = row.Value[3] ?? "";
                int rowIndex = importedRows.Keys.ToList().IndexOf(row.Key);

                //we create a new configuration element
                ElemConfig newElem = new ElemConfig();

                decimal dataLength = 0;
                if (row.Value[1].Contains("ARRAY"))
                {
                    previousWasArray = true;
                    newElem.ElemType = elemTypeDico.Keys.Contains(extractArrayType(row.Value[1])) ? elemTypeDico[extractArrayType(row.Value[1])] : ElemType.Undefined;
                    dataLength = ModbusUtils.GetDataLength(newElem.ElemType);

                    if (rowIndex > 0)
                    {
                        template.ElemGroups.Add(newElemenGroup);
                    }
                    newElemenGroup = new ElemGroupConfig();
                    newElemenGroup.DataBlock = newElem.ElemType == ElemType.Bool ? DataBlock.DiscreteInputs : DataBlock.HoldingRegisters;
                    newElemenGroup.Address = int.Parse(Regex.Replace(row.Key, @"[^0-9]", "")) - 1;

                    try
                    {
                        string[] arrayType = row.Value[1].Split(' ');
                        var elemType = elemTypeDico.Keys.Contains(arrayType[2]) ? elemTypeDico[arrayType[2]] : ElemType.Undefined;
                        int arrayLength = int.Parse(arrayType[0].Split("..")[1].Split(']')[0]) + 1;

                        for (int j = 0; j < arrayLength; j++)
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
                            newElemArray.Name = string.Format("{0}_{1}", row.Value[0], j);
                            newElemArray.TagCode = string.Format("{0}", decimal.Parse(row.Key) +(dataLength==1 ? dataLength * j:Math.Ceiling(dataLength * j / 2)));
                            newElemenGroup.Elems.Add(newElemArray);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error reading the file: one of the variables is an ARRAY but does not appear to be in the form 'ARRAY[0..X] OF TYPE'. Details: " + e.Message);
                    }
                    previousDataLength = dataLength;
                }
                else
                {
                    //if it isn't a child of a ghost row 
                    if (row.Key.Contains('.'))
                    {
                        continue;
                    }
                    else
                    {
                        //we set its properties according to imported row
                        string newType = elemTypeDico.Keys.Contains(row.Value[1]) ? row.Value[1] : cnlDataType.FirstOrDefault(t => t.Value == dataTypes.FirstOrDefault(dt => dt.Value == row.Value[1]).Key).Key;
                        newElem.ElemType = elemTypeDico.Keys.Contains(row.Value[1]) ? elemTypeDico[row.Value[1]] : ElemType.Undefined;
                        dataLength = ModbusUtils.GetDataLength(newElem.ElemType);
                        switch (dataLength)
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
                        int index = importedRows.Keys.ToList().IndexOf(row.Key);
                        int previousTagCodeAsNumber = 0;
                        int currentTagCodeAsNumber = 0;
                        if (!previousWasArray && index > 0)
                        {
                         previousTagCodeAsNumber = int.Parse(Regex.Replace(previousElem.TagCode, @"[^0-9]", ""));
                         currentTagCodeAsNumber = int.Parse(Regex.Replace(newElem.TagCode, @"[^0-9]", ""));
                        }
                        //if these conditions are met, we add the current element group to the template and create a new one
                        if (previousWasArray || index == 0 || prefix != previousPrefix || newElem.ElemType != previousType || previousTagCodeAsNumber + Math.Ceiling(previousDataLength / 2) != currentTagCodeAsNumber)//(prefix == "%MW" && newElemenGroup.Elems.Count == 125) || (prefix == "%M" && newElemenGroup.Elems.Count == 2000))
                        {
                            if (index > 0)
                            {
                                template.ElemGroups.Add(newElemenGroup);
                            }
                            newElemenGroup = new ElemGroupConfig();
                            newElemenGroup.DataBlock = newElem.ElemType == ElemType.Bool ? DataBlock.DiscreteInputs : DataBlock.HoldingRegisters;
                            newElemenGroup.Address = int.Parse(Regex.Replace(row.Key, @"[^0-9]", "")) - 1 + (textBox4.Text == "" ? 0 : int.Parse(textBox4.Text));
                        }
                        previousPrefix = prefix;
                        previousType = newElem.ElemType;

                        //we add the new element to the current element group
                        newElemenGroup.Elems.Add(newElem);
                        previousWasArray = false;
                    }
                    previousDataLength = dataLength;
                }
                previousElem = newElem;
            }
            template.ElemGroups.Add(newElemenGroup);
            for (int i = 0; i < template.ElemGroups.Count; i++)
            {
                if (template.ElemGroups[i].Elems.Count > 0)
                {
                    template.ElemGroups[i].Elems.Sort((x, y) => int.Parse(x.TagCode.Split('.')[0]) - int.Parse(y.TagCode.Split('.')[0]));
                    template.ElemGroups[i].Address = int.Parse(Regex.Replace(template.ElemGroups[i].Elems[0].TagCode, @"[^0-9]", "")) - 1 + (textBox4.Text == "" ? 0 : int.Parse(textBox4.Text));
                }
            }

            return template;
        }

        private string extractArrayType(string type)
        {
            return type.Contains("ARRAY") ? type.Split(' ')[2] : type;
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

            string NameWithoutPrefix(KeyValuePair<string, List<string>> row)
            {
                return selectedSuffixRowColumnIndex != null
                    ? selectedSuffixRowColumnIndex >= 0
                        ? string.Join(" - ", row.Value[0], extractArrayType(row.Value[selectedSuffixRowColumnIndex ?? 0]))
                        : string.Join(" - ", row.Value[0], row.Key)
                    : row.Value[0];
            }

            Func<Cnl, int, Cnl> updateChannel = (channel, i) =>
            {
                KeyValuePair<string, List<string>> correspondingRow = new KeyValuePair<string, List<string>>();
                if (importedRows.Keys.Contains(channel.TagCode))
                {
                    correspondingRow = new KeyValuePair<string, List<string>>(channel.TagCode, importedRows[channel.TagCode]);
                }
                else if (ghostArrayElementRow.Keys.Contains(channel.TagCode))
                {
                    correspondingRow = new KeyValuePair<string, List<string>>(channel.TagCode, ghostArrayElementRow[channel.TagCode]);
                }
                else
                {
                    return channel;
                }

                string newPrefix = selectedPrefixRowColumnIndex >= 0
                    ? extractArrayType(correspondingRow.Value[selectedPrefixRowColumnIndex ?? 0]) 
                    : channel.TagCode;
                channel.Name = selectedPrefixRowColumnIndex != null ?
                string.Format("{0} - {1}", newPrefix, NameWithoutPrefix(correspondingRow))

                :
                NameWithoutPrefix(correspondingRow);
                return channel;
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

            string NameWithoutSuffix(KeyValuePair<string, List<string>> row)
            {
                return selectedPrefixRowColumnIndex != null
                    ? selectedPrefixRowColumnIndex >= 0
                        ? string.Join(" - ", extractArrayType(row.Value[selectedPrefixRowColumnIndex ?? 0]), row.Value[0])
                        : string.Join(" - ", row.Key, row.Value[0])
                    : row.Value[0];
            }


            Func<Cnl, int, Cnl> updateChannel = (channel, i) =>
            {
                KeyValuePair<string, List<string>> correspondingRow = new KeyValuePair<string, List<string>>();
                if (importedRows.Keys.Contains(channel.TagCode))
                {
                    correspondingRow = new KeyValuePair<string, List<string>>(channel.TagCode, importedRows[channel.TagCode]);
                }
                else if (ghostArrayElementRow.Keys.Contains(channel.TagCode))
                {
                    correspondingRow = new KeyValuePair<string, List<string>>(channel.TagCode, ghostArrayElementRow[channel.TagCode]);
                }
                else
                {
                    return channel;
                }

                string newSuffix = selectedSuffixRowColumnIndex >= 0
                    ? extractArrayType(correspondingRow.Value[selectedSuffixRowColumnIndex ?? 0])
                    : channel.TagCode;
                channel.Name = selectedSuffixRowColumnIndex != null ?
                string.Format("{0} - {1}", NameWithoutSuffix(correspondingRow), selectedSuffixRowColumnIndex >= 0
                    ? newSuffix
                    : channel.TagCode)

                :
                NameWithoutSuffix(correspondingRow);
                return channel;
            };

            importedChannels = importedChannels.Select(updateChannel).ToList();
            channelsToCreateAfterMerge = channelsToCreateAfterMerge != null ? channelsToCreateAfterMerge.Select(updateChannel).ToList() : importedChannels.DeepClone();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // must be an int
            if (!int.TryParse(textBox4.Text, out _))
            {
                if (textBox4.Text != string.Empty)
                {
                    MessageBox.Show("Please enter a valid integer.");
                    textBox4.Text = string.Empty;
                }
            }
        }
    }
}
