using Scada.Admin.Extensions.ExtImport.Code;
using System.ComponentModel;

namespace Scada.Admin.Extensions.ExtImport.Forms
{
    partial class FrmCnlsMerge
    {
        //private DataGridView dataGridView;

        /// <summary> 
        /// 
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 
        /// </summary>
        /// <param name="disposing">
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants


        private void InitializeComponent()
        {
            label3 = new Label();
            dataGridView1 = new DataGridView();
            btnCancel = new Button();
            btnAdd = new Button();
            saveFileDialog1 = new SaveFileDialog();
            lblsrc = new Label();
            lbldest = new Label();
            Column1Txt = new DataGridViewTextBoxColumn();
            ColumnChk = new DataGridViewCheckBoxColumn();
            fcnlName = new DataGridViewTextBoxColumn();
            fdataType = new DataGridViewTextBoxColumn();
            fcnlType = new DataGridViewTextBoxColumn();
            fTagCode = new DataGridViewTextBoxColumn();
            ColumnVide = new DataGridViewTextBoxColumn();
            Column2Chk = new DataGridViewCheckBoxColumn();
            cnlName = new DataGridViewTextBoxColumn();
            dataType = new DataGridViewTextBoxColumn();
            cnlType = new DataGridViewTextBoxColumn();
            tagCode = new DataGridViewTextBoxColumn();
            ColumnvIDE2 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 27);
            label3.Name = "label3";
            label3.Size = new Size(0, 20);
            label3.TabIndex = 17;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1Txt, ColumnChk, fcnlName, fdataType, fcnlType, fTagCode, ColumnVide, Column2Chk, cnlName, dataType, cnlType, tagCode, ColumnvIDE2 });
            dataGridView1.Location = new Point(11, 44);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 180;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1671, 664);
            dataGridView1.TabIndex = 21;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(1547, 715);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(119, 35);
            btnCancel.TabIndex = 23;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(1408, 715);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(109, 35);
            btnAdd.TabIndex = 22;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click_1;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "Fichiers XML (*.xml)|*.xml";
            saveFileDialog1.RestoreDirectory = true;
            // 
            // lblsrc
            // 
            lblsrc.AutoSize = true;
            lblsrc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblsrc.Location = new Point(11, 9);
            lblsrc.Name = "lblsrc";
            lblsrc.Size = new Size(56, 20);
            lblsrc.TabIndex = 24;
            lblsrc.Text = "Source";
            // 
            // lbldest
            // 
            lbldest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbldest.AutoSize = true;
            lbldest.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbldest.Location = new Point(1592, 9);
            lbldest.Name = "lbldest";
            lbldest.Size = new Size(90, 20);
            lbldest.TabIndex = 25;
            lbldest.Text = "Destination";
            // 
            // Column1Txt
            // 
            Column1Txt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1Txt.HeaderText = "Column1Txt";
            Column1Txt.MinimumWidth = 6;
            Column1Txt.Name = "Column1Txt";
            // 
            // ColumnChk
            // 
            ColumnChk.HeaderText = "";
            ColumnChk.MinimumWidth = 6;
            ColumnChk.Name = "ColumnChk";
            ColumnChk.Resizable = DataGridViewTriState.True;
            ColumnChk.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // fcnlName
            // 
            fcnlName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fcnlName.HeaderText = "fcnlName";
            fcnlName.MinimumWidth = 6;
            fcnlName.Name = "fcnlName";
            // 
            // fdataType
            // 
            fdataType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fdataType.HeaderText = "fdataType";
            fdataType.MinimumWidth = 6;
            fdataType.Name = "fdataType";
            // 
            // fcnlType
            // 
            fcnlType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fcnlType.HeaderText = "fcnlType";
            fcnlType.MinimumWidth = 6;
            fcnlType.Name = "fcnlType";
            // 
            // fTagCode
            // 
            fTagCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fTagCode.HeaderText = "fTagCode";
            fTagCode.MinimumWidth = 6;
            fTagCode.Name = "fTagCode";
            // 
            // ColumnVide
            // 
            ColumnVide.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnVide.HeaderText = "";
            ColumnVide.MinimumWidth = 6;
            ColumnVide.Name = "ColumnVide";
            // 
            // Column2Chk
            // 
            Column2Chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2Chk.HeaderText = "";
            Column2Chk.MinimumWidth = 6;
            Column2Chk.Name = "Column2Chk";
            Column2Chk.Resizable = DataGridViewTriState.True;
            Column2Chk.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // cnlName
            // 
            cnlName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cnlName.HeaderText = "cnlName";
            cnlName.MinimumWidth = 6;
            cnlName.Name = "cnlName";
            // 
            // dataType
            // 
            dataType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataType.HeaderText = "dataType";
            dataType.MinimumWidth = 6;
            dataType.Name = "dataType";
            // 
            // cnlType
            // 
            cnlType.HeaderText = "cnlType";
            cnlType.MinimumWidth = 6;
            cnlType.Name = "cnlType";
            // 
            // tagCode
            // 
            tagCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tagCode.HeaderText = "tagCode";
            tagCode.MinimumWidth = 6;
            tagCode.Name = "tagCode";
            // 
            // ColumnvIDE2
            // 
            ColumnvIDE2.HeaderText = "";
            ColumnvIDE2.MinimumWidth = 6;
            ColumnvIDE2.Name = "ColumnvIDE2";
            // 
            // FrmCnlsMerge
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1694, 756);
            Controls.Add(lbldest);
            Controls.Add(lblsrc);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1440, 703);
            Name = "FrmCnlsMerge";
            Load += FrmCnlMerge_Load;
            ((ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private DataGridView dataGridView1;
        private Button btnCancel;
        private Button btnAdd;
        private SaveFileDialog saveFileDialog;
        private SaveFileDialog saveFileDialog1;
        private Label lblsrc;
        private Label lbldest;
        private DataGridViewTextBoxColumn Column1Txt;
        private DataGridViewCheckBoxColumn ColumnChk;
        private DataGridViewTextBoxColumn fcnlName;
        private DataGridViewTextBoxColumn fdataType;
        private DataGridViewTextBoxColumn fcnlType;
        private DataGridViewTextBoxColumn fTagCode;
        private DataGridViewTextBoxColumn ColumnVide;
        private DataGridViewCheckBoxColumn Column2Chk;
        private DataGridViewTextBoxColumn cnlName;
        private DataGridViewTextBoxColumn dataType;
        private DataGridViewTextBoxColumn cnlType;
        private DataGridViewTextBoxColumn tagCode;
        private DataGridViewTextBoxColumn ColumnvIDE2;
    }
}