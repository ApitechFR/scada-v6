using System.Text.RegularExpressions;
using Scada.Admin.Extensions.ExtImport.Code;
using Scada.Admin.Project;
using Scada.Data.Entities;

namespace Scada.Admin.Extensions.ExtImport.Forms
{
    public partial class FrmImportFile : Form
    {
        public Dictionary<string, List<string>> importedRows;
        public List<Cnl> chanelsToCreateAfterMerge;
        public string selectedFileName;
        public string selectedDeviceName;
        public List<Cnl> conflictualChanels;
        private bool conflictsAreSolved = false;

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
            button1.Enabled = false;
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
        /// Create a chanel object, filled with data from a row of the imported file
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="rowValue"></param>
        /// <returns></returns>
        public Cnl CreateChanelFromIncomingRow(string rowKey, List<string> rowValue)
        {
            Cnl cnl = new Cnl();
            cnl.Name = rowValue[0] + " (" + rowValue[2] + ")";
            cnl.TagCode = rowKey;
            if (ConfigDictionaries.CnlDataType.ContainsKey(rowValue[1]))
            {
                cnl.DataTypeID = ConfigDictionaries.CnlDataType[rowValue[1]];
            }
            //default Type is input/output
            cnl.CnlTypeID = 2; 
            //todo: cnl.DeviceNum = deviceNum;

            return cnl;
        }

        /// <summary>
        /// Hides or shows elements in the form, considering different verifications.
        /// </summary>
        private void refreshFormAccesses()
        {
            //check if device is selected and if file is chosen
            bool areFileAndDeviceSelected = selectedFileName != "";// && selectedDeviceName != null;

            //enable controls if file and device are selected, disbale otherwise
            cbBoxPrefix.Enabled = areFileAndDeviceSelected;
            cbBoxSuffix.Enabled = areFileAndDeviceSelected;
            button1.Enabled = areFileAndDeviceSelected;

            if (areFileAndDeviceSelected)
            {
                DetectConflicts();
                //We consider that there is a conflict if there are conflictual channels and if user has not already solved conflicts
                bool hasConflicts = conflictualChanels.Count() > 0 && !conflictsAreSolved;
                if (!hasConflicts && !conflictsAreSolved)
                {
                    chanelsToCreateAfterMerge = importedRows.Select(r => CreateChanelFromIncomingRow(r.Key, r.Value)).ToList();
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
            conflictualChanels = project.ConfigDatabase.CnlTable.Where(chanel => importedRows.ContainsKey(chanel.TagCode)).ToList();
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
        private void CreateChanels(List<Cnl> cnls)
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
            FrmCnlsMergeCopy frmCnlsMerge = new(project, conflictualChanels, importedRows, 1);

            //If the merge form is closed with OK, we update the chanels to create
            if (frmCnlsMerge.ShowDialog() == DialogResult.OK)
            {
                chanelsToCreateAfterMerge = frmCnlsMerge.chanelsToCreate;
                conflictsAreSolved = true;

                //We refresh the display of the form
                refreshFormAccesses();
            }
        }

        /// <summary>
        /// On click on OK button, create chanels in project according to merge form result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //First, we create channels chosen in merge form
            CreateChanels(chanelsToCreateAfterMerge);

            //If not all imported rows were in conflict, we create chanels for all other rows
            if(importedRows.Count() != importedRows.Where(r => conflictualChanels.Any(c => c.TagCode == r.Key)).Count() && conflictualChanels.Count()>0)
            {
                // we create channels for all other rows, which were not in conflict
                List<Cnl> nonConflictualChanels = importedRows.Where(r=>!conflictualChanels.Any(c=>c.TagCode == r.Key)).Select(r => CreateChanelFromIncomingRow(r.Key, r.Value)).ToList();
                //we add them to the project's chanels
                CreateChanels(nonConflictualChanels);
            }

            //Finally, we close the form
            DialogResult = DialogResult.OK;
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
    }
}
