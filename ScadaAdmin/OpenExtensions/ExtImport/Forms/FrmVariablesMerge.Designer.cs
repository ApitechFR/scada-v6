using System.ComponentModel;
using Scada.Admin.Extensions.ExtImport.Code;

namespace Scada.Admin.Extensions.ExtImport.Forms
{
    partial class FrmVariablesMerge
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
            checkBox3 = new CheckBox();
            dataGridView1 = new DataGridView();
            Column1Txt = new DataGridViewTextBoxColumn();
            ColumnChk = new DataGridViewCheckBoxColumn();
            Column2Txt = new DataGridViewTextBoxColumn();
            Column33 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            ColumnVide = new DataGridViewTextBoxColumn();
            Column2Chk = new DataGridViewCheckBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column44 = new DataGridViewTextBoxColumn();
            btnCancel = new Button();
            saveFileDialog1 = new SaveFileDialog();
            button1 = new Button();
            lblsrc = new Label();
            lbldest = new Label();
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
            // checkBox3
            // 
            checkBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(11, 628);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(18, 17);
            checkBox3.TabIndex = 18;
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1Txt, ColumnChk, Column2Txt, Column33, Column4, ColumnVide, Column2Chk, Column2, Column3, Column44 });
            dataGridView1.Location = new Point(11, 57);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 180;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1398, 557);
            dataGridView1.TabIndex = 21;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Column1Txt
            // 
            Column1Txt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            // Column2Txt
            // 
            Column2Txt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2Txt.MinimumWidth = 6;
            Column2Txt.Name = "Column2Txt";
            // 
            // Column33
            // 
            Column33.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column33.MinimumWidth = 6;
            Column33.Name = "Column33";
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
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
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            // 
            // Column44
            // 
            Column44.MinimumWidth = 6;
            Column44.Name = "Column44";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(1328, 620);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 35);
            btnCancel.TabIndex = 23;
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "Fichiers XML (*.xml)|*.xml";
            saveFileDialog1.RestoreDirectory = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(1221, 620);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(101, 35);
            button1.TabIndex = 26;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblsrc
            // 
            lblsrc.AutoSize = true;
            lblsrc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblsrc.Location = new Point(12, 27);
            lblsrc.Name = "lblsrc";
            lblsrc.Size = new Size(56, 20);
            lblsrc.TabIndex = 27;
            lblsrc.Text = "Source";
            // 
            // lbldest
            // 
            lbldest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbldest.AutoSize = true;
            lbldest.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbldest.Location = new Point(1324, 27);
            lbldest.Name = "lbldest";
            lbldest.Size = new Size(90, 20);
            lbldest.TabIndex = 28;
            lbldest.Text = "Destination";
            // 
            // FrmVariablesMerge
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1424, 669);
            Controls.Add(lbldest);
            Controls.Add(lblsrc);
            Controls.Add(button1);
            Controls.Add(btnCancel);
            Controls.Add(dataGridView1);
            Controls.Add(checkBox3);
            Controls.Add(label3);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1440, 703);
            Name = "FrmVariablesMerge";
            Load += FrmCnlMerge_Load;
            ((ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private CheckBox checkBox3;
        private DataGridView dataGridView1;
        private Button btnCancel;
        private DataGridViewTextBoxColumn Column1Txt;
        private DataGridViewCheckBoxColumn ColumnChk;
        private DataGridViewTextBoxColumn Column2Txt;
        private DataGridViewTextBoxColumn Column33;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn ColumnVide;
        private DataGridViewCheckBoxColumn Column2Chk;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column44;
        private SaveFileDialog saveFileDialog;
        private SaveFileDialog saveFileDialog1;
        private Button button1;
        private Label lblsrc;
        private Label lbldest;
    }
}