
using Scada.Admin.Project;
using Scada.Data.Entities;
using Scada.Admin.Extensions.ExtImport.Code;
using Scada.Comm.Devices;
using System.Xml.Linq;
using Scada.Forms;
using static Scada.Admin.Extensions.ExtImport.Forms.FrmCnlsMerge;


namespace Scada.Admin.Extensions.ExtImport.Forms
{
    public partial class FrmCnlsMerge : Form
    {

        private List<Cnl> currentChannels;
        public List<Cnl> channelsToCreate;
        private List<Cnl> incomingChannels;
        private int deviceNum;
        private IAdminContext adminContext; // the Administrator context
        private ScadaProject project;       // the project under development
        private CheckBox _headerCheckBox1 = new CheckBox();
        private CheckBox _headerCheckBox2 = new CheckBox();

        private readonly Dictionary<int, string> cnlTypeDictionary = ConfigDictionaries.CnlTypeDictionary;
        private readonly Dictionary<int, string> dataTypeDictionary = ConfigDictionaries.DataTypeDictionary;

        List<RowData> rowDataList = new List<RowData>();

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

        public FrmCnlsMerge(ScadaProject project, List<Cnl> currentChannels, List<Cnl> incomingChannels)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            this.project = project;
            this.incomingChannels = incomingChannels;
            this.currentChannels = currentChannels;

            FillGridView();
        }

        public void FillGridView()
        {
            dataGridView1.Rows.Clear();

            foreach (var incomingChannel in incomingChannels)
            {
                var sameCodeCurrentChannels = currentChannels.FirstOrDefault(cnl => cnl.TagCode == incomingChannel.TagCode);
                if (sameCodeCurrentChannels != null)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    incomingChannel.CnlNum = sameCodeCurrentChannels.CnlNum;

                    //Cells from 0 to 5 are for incoming row
                    row.Cells[0].Value = sameCodeCurrentChannels.CnlNum;
                    row.Cells[1].Value = false;
                    row.Cells[2].Value = incomingChannel.Name;
                    row.Cells[3].Value = (incomingChannel.DataTypeID.HasValue) ? dataTypeDictionary[incomingChannel.DataTypeID.Value] : "";
                    row.Cells[4].Value = cnlTypeDictionary[incomingChannel.CnlTypeID];
                    row.Cells[5].Value = incomingChannel.TagCode;

                    //Cell 6 contains incomingRow as a channel
                    row.Cells[6].Value = incomingChannel;

                    //Cells from 7 to 11 are for current row
                    row.Cells[7].Value = false;
                    row.Cells[8].Value = sameCodeCurrentChannels.Name;
                    row.Cells[9].Value = (sameCodeCurrentChannels.DataTypeID.HasValue) ? dataTypeDictionary[sameCodeCurrentChannels.DataTypeID.Value] : "";
                    row.Cells[10].Value = cnlTypeDictionary[sameCodeCurrentChannels.CnlTypeID];
                    row.Cells[11].Value = sameCodeCurrentChannels.TagCode;

                    //Cell 12 contains cnl as a channel
                    row.Cells[12].Value = sameCodeCurrentChannels;
                }

            }
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[12].Visible = false;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                rowDataList.Add(new RowData());
            }
        }

        public class RowData
        {
            public bool CheckBox1Value { get; set; }
            public bool CheckBox2Value { get; set; }

            public RowData()
            {
                CheckBox1Value = false;
                CheckBox2Value = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                RowData rowData = rowDataList[e.RowIndex];

                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (e.ColumnIndex == 1)
                    {
                        if (rowData.CheckBox1Value)
                            rowData.CheckBox1Value = false;

                        else if (!rowData.CheckBox1Value)
                            rowData.CheckBox1Value = true;

                        if (rowData.CheckBox1Value)
                        {
                            rowData.CheckBox2Value = false;
                            dataGridView1[7, e.RowIndex].Value = false;
                        }
                    }
                    else if (e.ColumnIndex == 7)
                    {
                        if (rowData.CheckBox2Value)
                            rowData.CheckBox2Value = false;

                        else if (!rowData.CheckBox2Value)
                            rowData.CheckBox2Value = true;

                        if (rowData.CheckBox2Value)
                        {
                            rowData.CheckBox1Value = false;
                            dataGridView1[1, e.RowIndex].Value = false;
                        }
                    }

                    UpdateCellColors(e.RowIndex);
                }
            }
            dataGridView1.ClearSelection();
            dataGridView1.EndEdit();
        }

        private void UpdateCellColors(int rowIndex)
        {
            RowData rowData = rowDataList[rowIndex];
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            if (rowData.CheckBox1Value && !rowData.CheckBox2Value)
            {
                UpdateRowColors(row, Color.LightGreen, Color.PaleVioletRed);
            }
            else if (!rowData.CheckBox1Value && rowData.CheckBox2Value)
            {
                UpdateRowColors(row, Color.PaleVioletRed, Color.LightGreen);
            }
            else
            {
                // 0 cell checked
                UpdateRowColors(row, Color.White, Color.White);
            }
        }

        private void UpdateRowColors(DataGridViewRow row, Color color1, Color color2)
        {
            row.Cells[1].Style.BackColor = color1;
            row.Cells[2].Style.BackColor = color1;
            row.Cells[3].Style.BackColor = color1;
            row.Cells[4].Style.BackColor = color1;
            row.Cells[5].Style.BackColor = color1;

            row.Cells[7].Style.BackColor = color2;
            row.Cells[8].Style.BackColor = color2;
            row.Cells[9].Style.BackColor = color2;
            row.Cells[10].Style.BackColor = color2;
            row.Cells[11].Style.BackColor = color2;
        }

        private void _headerCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_headerCheckBox1.Checked)
            {
                _headerCheckBox2.Checked = false;

                foreach (RowData row in rowDataList)
                {
                    if (dataGridView1.Columns[1] is DataGridViewCheckBoxColumn && dataGridView1.Columns[7] is DataGridViewCheckBoxColumn)
                    {
                        dataGridView1[1, rowDataList.IndexOf(row)].Value = true;
                        dataGridView1[7, rowDataList.IndexOf(row)].Value = false;
                        row.CheckBox1Value = true;
                        row.CheckBox2Value = false;
                        UpdateCellColors(rowDataList.IndexOf(row));
                        dataGridView1.EndEdit();
                    }
                }
            }
            else
            {
                foreach (RowData row in rowDataList)
                {
                    if (dataGridView1.Columns[1] is DataGridViewCheckBoxColumn && dataGridView1.Columns[7] is DataGridViewCheckBoxColumn)
                    {
                        dataGridView1[1, rowDataList.IndexOf(row)].Value = false;
                        dataGridView1[7, rowDataList.IndexOf(row)].Value = false;
                        row.CheckBox1Value = false;
                        row.CheckBox2Value = false;
                        UpdateCellColors(rowDataList.IndexOf(row));
                        dataGridView1.EndEdit();
                    }
                }
            }

            dataGridView1.EndEdit();
        }

        private void _headerCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_headerCheckBox2.Checked)
            {
                _headerCheckBox1.Checked = false;

                foreach (RowData row in rowDataList)
                {
                    if (dataGridView1.Columns[1] is DataGridViewCheckBoxColumn && dataGridView1.Columns[7] is DataGridViewCheckBoxColumn)
                    {
                        dataGridView1[1, rowDataList.IndexOf(row)].Value = false;
                        dataGridView1[7, rowDataList.IndexOf(row)].Value = true;
                        row.CheckBox2Value = true;
                        row.CheckBox1Value = false;
                        UpdateCellColors(rowDataList.IndexOf(row));
                        dataGridView1.EndEdit();
                    }
                }
            }
            else
            {
                foreach (RowData row in rowDataList)
                {
                    if (dataGridView1.Columns[1] is DataGridViewCheckBoxColumn && dataGridView1.Columns[7] is DataGridViewCheckBoxColumn)
                    {
                        dataGridView1[7, rowDataList.IndexOf(row)].Value = false;
                        dataGridView1[1, rowDataList.IndexOf(row)].Value = false;
                        row.CheckBox2Value = false;
                        row.CheckBox1Value = false;
                        UpdateCellColors(rowDataList.IndexOf(row));
                        dataGridView1.EndEdit();
                    }
                }
            }
            dataGridView1.EndEdit();
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
            channelsToCreate = new List<Cnl>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == true)
                {
                    Cnl cnl = (Cnl)row.Cells[6].Value;
                    channelsToCreate.Add(cnl);
                }
                else if (Convert.ToBoolean(row.Cells[7].Value) == true)
                {
                    Cnl cnl = (Cnl)row.Cells[12].Value;
                    channelsToCreate.Add(cnl);
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                RowData rowData = rowDataList[e.RowIndex];

                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (e.ColumnIndex == 1)
                    {
                        if (rowData.CheckBox1Value)
                            rowData.CheckBox1Value = false;

                        else if (!rowData.CheckBox1Value)
                            rowData.CheckBox1Value = true;

                        if (rowData.CheckBox1Value)
                        {
                            rowData.CheckBox2Value = false;
                            dataGridView1[7, e.RowIndex].Value = false;
                        }
                    }
                    else if (e.ColumnIndex == 7)
                    {
                        if (rowData.CheckBox2Value)
                            rowData.CheckBox2Value = false;

                        else if (!rowData.CheckBox2Value)
                            rowData.CheckBox2Value = true;

                        if (rowData.CheckBox2Value)
                        {
                            rowData.CheckBox1Value = false;
                            dataGridView1[1, e.RowIndex].Value = false;
                        }
                    }

                    UpdateCellColors(e.RowIndex);
                }
            }
            dataGridView1.ClearSelection();
            dataGridView1.EndEdit();
        }
    }
}
