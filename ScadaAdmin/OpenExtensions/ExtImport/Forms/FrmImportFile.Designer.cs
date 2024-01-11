namespace Scada.Admin.Extensions.ExtImport.Forms
{
    partial class FrmImportFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPathFile = new TextBox();
            btnSelectFile = new Button();
            lblDevice = new Label();
            cbDevice = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            cbBoxSuffix = new ComboBox();
            cbBoxPrefix = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label4 = new Label();
            pbStatus = new PictureBox();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            label8 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label9 = new Label();
            textBox3 = new TextBox();
            label10 = new Label();
            panel4 = new Panel();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // txtPathFile
            // 
            txtPathFile.Enabled = false;
            txtPathFile.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtPathFile.Location = new Point(12, 26);
            txtPathFile.Margin = new Padding(4, 4, 3, 2);
            txtPathFile.Name = "txtPathFile";
            txtPathFile.ReadOnly = true;
            txtPathFile.Size = new Size(196, 23);
            txtPathFile.TabIndex = 10;
            txtPathFile.TabStop = false;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectFile.ForeColor = SystemColors.ActiveCaptionText;
            btnSelectFile.ImageAlign = ContentAlignment.TopCenter;
            btnSelectFile.Location = new Point(218, 25);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(32, 24);
            btnSelectFile.TabIndex = 9;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click_1;
            // 
            // lblDevice
            // 
            lblDevice.AutoSize = true;
            lblDevice.Location = new Point(12, 7);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new Size(25, 15);
            lblDevice.TabIndex = 11;
            lblDevice.Text = "File";
            // 
            // cbDevice
            // 
            cbDevice.DisplayMember = "Name";
            cbDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDevice.FormattingEnabled = true;
            cbDevice.Location = new Point(12, 71);
            cbDevice.Name = "cbDevice";
            cbDevice.Size = new Size(238, 23);
            cbDevice.TabIndex = 13;
            cbDevice.ValueMember = "DeviceNum";
            cbDevice.SelectedIndexChanged += cbDevice_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 53);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 12;
            label1.Text = "Device";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 3);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 18;
            label3.Text = "Prefix ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(216, 2);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 17;
            label2.Text = "Suffix";
            // 
            // cbBoxSuffix
            // 
            cbBoxSuffix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxSuffix.FormattingEnabled = true;
            cbBoxSuffix.Location = new Point(218, 20);
            cbBoxSuffix.Margin = new Padding(3, 2, 3, 2);
            cbBoxSuffix.Name = "cbBoxSuffix";
            cbBoxSuffix.Size = new Size(197, 23);
            cbBoxSuffix.TabIndex = 16;
            cbBoxSuffix.SelectedIndexChanged += cbBoxSuffix_SelectedIndexChanged;
            // 
            // cbBoxPrefix
            // 
            cbBoxPrefix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxPrefix.FormattingEnabled = true;
            cbBoxPrefix.Location = new Point(0, 20);
            cbBoxPrefix.Margin = new Padding(3, 2, 3, 2);
            cbBoxPrefix.Name = "cbBoxPrefix";
            cbBoxPrefix.Size = new Size(195, 23);
            cbBoxPrefix.TabIndex = 15;
            cbBoxPrefix.SelectedIndexChanged += cbBoxPrefix_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(341, 258);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(103, 22);
            button1.TabIndex = 19;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(219, 258);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(105, 22);
            button2.TabIndex = 20;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(-1, 26);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(237, 22);
            button3.TabIndex = 21;
            button3.Text = "Solve conflicts";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 4);
            label4.Name = "label4";
            label4.Size = new Size(190, 15);
            label4.TabIndex = 22;
            label4.Text = "Some conflicts have to be resolved";
            // 
            // pbStatus
            // 
            pbStatus.Image = Properties.Resources.success;
            pbStatus.Location = new Point(0, 4);
            pbStatus.Name = "pbStatus";
            pbStatus.Size = new Size(16, 16);
            pbStatus.TabIndex = 23;
            pbStatus.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 4);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 24;
            label5.Text = "No conflict";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.warning;
            pictureBox1.Location = new Point(0, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(16, 16);
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cbBoxPrefix);
            panel1.Controls.Add(cbBoxSuffix);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(13, 180);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(431, 57);
            panel1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(button3);
            panel2.Location = new Point(13, 113);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(237, 48);
            panel2.TabIndex = 27;
            // 
            // panel3
            // 
            panel3.Controls.Add(label5);
            panel3.Controls.Add(pbStatus);
            panel3.Location = new Point(76, 126);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(109, 73);
            panel3.TabIndex = 28;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(273, 8);
            label8.Name = "label8";
            label8.Size = new Size(60, 15);
            label8.TabIndex = 19;
            label8.Text = "ByteOrder";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(59, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(96, 23);
            textBox1.TabIndex = 29;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(59, 56);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(96, 23);
            textBox2.TabIndex = 31;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(9, 21);
            label9.Name = "label9";
            label9.Size = new Size(44, 15);
            label9.TabIndex = 30;
            label9.Text = "2 Bytes";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(59, 95);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(96, 23);
            textBox3.TabIndex = 33;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(9, 59);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 32;
            label10.Text = "4 Bytes";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(224, 224, 224);
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label6);
            panel4.Controls.Add(textBox3);
            panel4.Controls.Add(textBox1);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(textBox2);
            panel4.Controls.Add(label9);
            panel4.Location = new Point(273, 26);
            panel4.Name = "panel4";
            panel4.Size = new Size(171, 135);
            panel4.TabIndex = 34;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 99);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 35;
            label6.Text = "8 Bytes";
            // 
            // FrmImportFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 291);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(label8);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(cbDevice);
            Controls.Add(label1);
            Controls.Add(lblDevice);
            Controls.Add(txtPathFile);
            Controls.Add(btnSelectFile);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmImportFile";
            ((System.ComponentModel.ISupportInitialize)pbStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPathFile;
        private Button btnSelectFile;
        private Label lblDevice;
        private ComboBox cbDevice;
        private Label label1;
        private Label label3;
        private Label label2;
        private ComboBox cbBoxSuffix;
        private ComboBox cbBoxPrefix;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label4;
        private PictureBox pbStatus;
        private Label label5;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label9;
        private TextBox textBox3;
        private Label label10;
        private Panel panel4;
    }
}