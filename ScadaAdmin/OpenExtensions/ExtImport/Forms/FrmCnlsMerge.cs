
using Scada.Admin.Project;
using Scada.Data.Entities;
using Scada.Admin.Extensions.ExtImport.Code;
using Scada.Comm.Devices;
using System.Xml.Linq;
using Scada.Forms;


namespace Scada.Admin.Extensions.ExtImport.Forms
{
    public partial class FrmCnlsMerge : Form
    {

        private List<Cnl> currentChanels;
        public List<Cnl> chanelsToCreate;
        private List<Cnl> incomingChanels;
        private int deviceNum;
        private IAdminContext adminContext; // the Administrator context
        private ScadaProject project;       // the project under development
        private CheckBox _headerCheckBox1 = new CheckBox();
        private CheckBox _headerCheckBox2 = new CheckBox();

        private readonly Dictionary<int, string> cnlTypeDictionary = ConfigDictionaries.CnlTypeDictionary;
        private readonly Dictionary<int, string> dataTypeDictionary = ConfigDictionaries.DataTypeDictionary;


        private FrmCnlsMerge()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        public void Init(IAdminContext adminContext, ScadaProject project)
        {
            this.adminContext = adminContext ?? throw new ArgumentNullException(nameof(adminContext));
            this.project = project ?? throw new ArgumentNullException(nameof(project));
        }

        public FrmCnlsMerge(ScadaProject project, List<Cnl> currentChanels, List<Cnl> incomingChanels)// Controls.CtrlImport1 ctrlImport1, Controls.CtrlImport2 ctrlImport2, Controls.CtrlImport3 ctrlImport3) : this()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            this.project = project;
            this.incomingChanels = incomingChanels;
            this.currentChanels = currentChanels;

            FillGridView();
        }

        public void FillGridView()
        {
            dataGridView1.Rows.Clear();

            foreach(var incomingChanel in incomingChanels)
            {
                var sameCodeCurrentChanels = currentChanels.Where(cnl => cnl.TagCode == incomingChanel.TagCode).ToList();

                foreach (var cnl in sameCodeCurrentChanels)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    incomingChanel.CnlNum = cnl.CnlNum;
                    //incomingChanel.DeviceNum = null;

                    //Cells from 0 to 5 are for incoming row
                    row.Cells[0].Value = cnl.CnlNum;
                    row.Cells[1].Value = false;
                    row.Cells[2].Value = incomingChanel.Name;
                    row.Cells[3].Value = (incomingChanel.DataTypeID.HasValue) ? dataTypeDictionary[incomingChanel.DataTypeID.Value] : "";
                    row.Cells[4].Value = cnlTypeDictionary[incomingChanel.CnlTypeID];
                    row.Cells[5].Value = incomingChanel.TagCode;

                    //Cell 6 contains incomingRow as a chanel
                    row.Cells[6].Value = incomingChanel;

                    //Cells from 7 to 11 are for current row
                    row.Cells[7].Value = false;
                    row.Cells[8].Value = cnl.Name;
                    row.Cells[9].Value = (cnl.DataTypeID.HasValue) ? dataTypeDictionary[cnl.DataTypeID.Value] : "";
                    row.Cells[10].Value = cnlTypeDictionary[cnl.CnlTypeID];
                    row.Cells[11].Value = cnl.TagCode;

                }
            }
            dataGridView1.Columns[6].Visible = false;
        }

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell currentCheckbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewCheckBoxCell otherCheckbox = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 7 : 1];

                if ((bool)currentCheckbox.Value == true && (bool)otherCheckbox.Value == false)
                {
                    currentCheckbox.Value = false;

                    otherCheckbox.Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 4].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 5].Style.BackColor = Color.White;
                    currentCheckbox.Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 8 : 2].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 9 : 3].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 10 : 4].Style.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 11 : 5].Style.BackColor = Color.White;
                }
                else
                {
                    if ((bool)currentCheckbox.Value == true)
                    {
                        otherCheckbox.Value = true;
                        currentCheckbox.Value = false;

                        otherCheckbox.Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 4].Style.BackColor = Color.LightGreen;

                        currentCheckbox.Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 0 ? 8 : 2].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 0 ? 9 : 3].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 0 ? 10 : 4].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 0 ? 11 : 5].Style.BackColor = Color.PaleVioletRed;
                    }
                    else
                    {
                        otherCheckbox.Value = false;
                        currentCheckbox.Value = true;

                        otherCheckbox.Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 8 : 2].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 9 : 3].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 10 : 4].Style.BackColor = Color.PaleVioletRed;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex == 1 ? 11 : 5].Style.BackColor = Color.PaleVioletRed;

                        currentCheckbox.Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Style.BackColor = Color.LightGreen;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 4].Style.BackColor = Color.LightGreen;

                    }
                }

            }
        }


        private void _headerCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_headerCheckBox1.Checked)
            {
                _headerCheckBox2.Checked = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.Cells[1].ReadOnly)
                    {
                        row.Cells[1].Value = true;

                        row.Cells[1].Style.BackColor = Color.LightGreen;
                        row.Cells[2].Style.BackColor = Color.LightGreen;
                        row.Cells[3].Style.BackColor = Color.LightGreen;
                        row.Cells[4].Style.BackColor = Color.LightGreen;
                        row.Cells[5].Style.BackColor = Color.LightGreen;

                        row.Cells[7].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[8].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[9].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[10].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[11].Style.BackColor = Color.PaleVioletRed;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.Cells[1].ReadOnly)
                    {
                        row.Cells[1].Value = false;

                        row.Cells[1].Style.BackColor = Color.White;
                        row.Cells[2].Style.BackColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.White;
                        row.Cells[4].Style.BackColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.White;

                        row.Cells[7].Style.BackColor = Color.White;
                        row.Cells[8].Style.BackColor = Color.White;
                        row.Cells[9].Style.BackColor = Color.White;
                        row.Cells[10].Style.BackColor = Color.White;
                        row.Cells[11].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void _headerCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_headerCheckBox2.Checked)
            {
                _headerCheckBox1.Checked = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.Cells[7].ReadOnly)
                    {
                        row.Cells[7].Value = true;

                        row.Cells[1].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[2].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[3].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[4].Style.BackColor = Color.PaleVioletRed;
                        row.Cells[5].Style.BackColor = Color.PaleVioletRed;

                        row.Cells[7].Style.BackColor = Color.LightGreen;
                        row.Cells[8].Style.BackColor = Color.LightGreen;
                        row.Cells[9].Style.BackColor = Color.LightGreen;
                        row.Cells[10].Style.BackColor = Color.LightGreen;
                        row.Cells[11].Style.BackColor = Color.LightGreen;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.Cells[7].ReadOnly)
                    {
                        row.Cells[7].Value = false;

                        row.Cells[1].Style.BackColor = Color.White;
                        row.Cells[2].Style.BackColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.White;
                        row.Cells[4].Style.BackColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.White;

                        row.Cells[7].Style.BackColor = Color.White;
                        row.Cells[8].Style.BackColor = Color.White;
                        row.Cells[9].Style.BackColor = Color.White;
                        row.Cells[10].Style.BackColor = Color.White;
                        row.Cells[11].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void FrmCnlMerge_Load(object sender, EventArgs e)
        {
            SetCheckboxLocation(_headerCheckBox1, 1);
            SetCheckboxLocation(_headerCheckBox2, 7);

            dataGridView1.Controls.Add(_headerCheckBox1);
            _headerCheckBox1.CheckedChanged += _headerCheckBox1_CheckedChanged;
            dataGridView1.Controls.Add(_headerCheckBox2);
            _headerCheckBox2.CheckedChanged += _headerCheckBox2_CheckedChanged;
            dataGridView1.ColumnWidthChanged += DataGridView1_ColumnWidthChanged;


        }

        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            SetCheckboxLocation(_headerCheckBox1, 1);
            SetCheckboxLocation(_headerCheckBox2, 7);
        }

        private void SetCheckboxLocation(System.Windows.Forms.CheckBox ck, int columnIndex)
        {
            Rectangle headerCellRectangle = this.dataGridView1.GetCellDisplayRectangle(columnIndex, -1, true);

            ck.Location = new Point(headerCellRectangle.X + (headerCellRectangle.Width / 2) - 8, headerCellRectangle.Y + 2);
            ck.BackColor = Color.Transparent;
            ck.Size = new Size(18, 18);
        }

        /// <summary>
        /// Create or update cnl from file
        /// </summary>
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            chanelsToCreate = new List<Cnl>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == true)
                {
                    Cnl cnl = (Cnl)row.Cells[6].Value;
                    chanelsToCreate.Add(cnl);
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
