using System.Text.RegularExpressions;
using Scada.Admin.Project;

namespace Scada.Admin.Extensions.ExtImport.Forms
{
    public partial class FrmImportFile : Form
    {
        public Dictionary<string, List<string>> rowsToImport;
        public string selectedFileName;
        public string selectedDeviceName;
        public List<Scada.Data.Entities.Cnl> conflictualChanels;
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
            rowsToImport = new Dictionary<string, List<string>>();
        }

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
                    refreshFormAccesses();
                }
            }
        }

        private void readFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                if (Path.GetExtension(fileName) == ".txt" || Path.GetExtension(fileName) == ".TXT")
                {
                    bool isFirstLine = true;
                    bool isPL7 = false;
                    rowsToImport.Clear();

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
        /// Hides or shows elements in the form, considering different verifications.
        /// </summary>

        private void refreshFormAccesses()
        {
            //check if device is selected and if file is chosen
            bool areFileAndDeviceSelected = selectedFileName != "";// && selectedDeviceName != null;

            //enable prefix and suffix selection if file and deviceare selected, disbale otherwise
            cbBoxPrefix.Enabled = areFileAndDeviceSelected;
            cbBoxSuffix.Enabled = areFileAndDeviceSelected;

            if (areFileAndDeviceSelected)
            {
                //update detectedConflicts
                DetectConflicts();
                bool hasConflicts = conflictualChanels.Count() > 0;
                //hide and display conflicts related labels and button container
                panel2.Visible= hasConflicts;
                panel3.Visible= !hasConflicts;
                button1.Enabled = !hasConflicts;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DetectConflicts()
        {
            //find channels with same adress in project
            conflictualChanels = project.ConfigDatabase.CnlTable.Where(chanel => rowsToImport.ContainsKey(chanel.TagCode)).ToList();
        }

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
            if (rowsToImport.ContainsKey(adress))
            {
                return;
            }

            string prefix = Regex.Split(columns[adressIndex], @"[0-9]").First();

            this.rowsToImport.Add(adress, new List<string>
            {
                columns[mnemoniqueIndex],
                columns[typeIndex],
                columns[commentIndex].Replace("\"", ""),
                prefix
            });
        }

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

                rowsToImport.Add(adress, list);
            }
        }

        /// <summary>
        /// Action triggered on click on "resolve conflicts" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FrmCnlsMergeCopy frmCnlsMerge = new(project, conflictualChanels, rowsToImport, 1);
            frmCnlsMerge.ShowDialog();
        }
    }
}
