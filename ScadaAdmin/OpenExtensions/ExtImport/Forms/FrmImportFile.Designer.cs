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
            panel5 = new Panel();
            label7 = new Label();
            textBox4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pbStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // txtPathFile
            // 
            txtPathFile.Enabled = false;
            txtPathFile.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtPathFile.Location = new Point(14, 35);
            txtPathFile.Margin = new Padding(5, 5, 3, 3);
            txtPathFile.Name = "txtPathFile";
            txtPathFile.ReadOnly = true;
            txtPathFile.Size = new Size(223, 27);
            txtPathFile.TabIndex = 10;
            txtPathFile.TabStop = false;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectFile.ForeColor = SystemColors.ActiveCaptionText;
            btnSelectFile.ImageAlign = ContentAlignment.TopCenter;
            btnSelectFile.Location = new Point(249, 33);
            btnSelectFile.Margin = new Padding(3, 4, 3, 4);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(37, 32);
            btnSelectFile.TabIndex = 9;
            btnSelectFile.Text = "...";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click_1;
            // 
            // lblDevice
            // 
            lblDevice.AutoSize = true;
            lblDevice.Location = new Point(14, 9);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new Size(32, 20);
            lblDevice.TabIndex = 11;
            lblDevice.Text = "File";
            // 
            // cbDevice
            // 
            cbDevice.DisplayMember = "Name";
            cbDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDevice.FormattingEnabled = true;
            cbDevice.Location = new Point(14, 95);
            cbDevice.Margin = new Padding(3, 4, 3, 4);
            cbDevice.Name = "cbDevice";
            cbDevice.Size = new Size(271, 28);
            cbDevice.TabIndex = 13;
            cbDevice.ValueMember = "DeviceNum";
            cbDevice.SelectedIndexChanged += cbDevice_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 71);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 12;
            label1.Text = "Device";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 3);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 18;
            label3.Text = "Prefix ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(247, 3);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 17;
            label2.Text = "Suffix";
            // 
            // cbBoxSuffix
            // 
            cbBoxSuffix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxSuffix.FormattingEnabled = true;
            cbBoxSuffix.Location = new Point(249, 27);
            cbBoxSuffix.Name = "cbBoxSuffix";
            cbBoxSuffix.Size = new Size(225, 28);
            cbBoxSuffix.TabIndex = 16;
            cbBoxSuffix.SelectedIndexChanged += cbBoxSuffix_SelectedIndexChanged;
            // 
            // cbBoxPrefix
            // 
            cbBoxPrefix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxPrefix.FormattingEnabled = true;
            cbBoxPrefix.Location = new Point(3, 27);
            cbBoxPrefix.Name = "cbBoxPrefix";
            cbBoxPrefix.Size = new Size(222, 28);
            cbBoxPrefix.TabIndex = 15;
            cbBoxPrefix.SelectedIndexChanged += cbBoxPrefix_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(389, 399);
            button1.Name = "button1";
            button1.Size = new Size(118, 29);
            button1.TabIndex = 19;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(249, 399);
            button2.Name = "button2";
            button2.Size = new Size(120, 29);
            button2.TabIndex = 20;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(-1, 35);
            button3.Name = "button3";
            button3.Size = new Size(271, 29);
            button3.TabIndex = 21;
            button3.Text = "Solve conflicts";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 5);
            label4.Name = "label4";
            label4.Size = new Size(240, 20);
            label4.TabIndex = 22;
            label4.Text = "Some conflicts have to be resolved";
            // 
            // pbStatus
            // 
            pbStatus.Image = Properties.Resources.success;
            pbStatus.Location = new Point(0, 5);
            pbStatus.Margin = new Padding(3, 4, 3, 4);
            pbStatus.Name = "pbStatus";
            pbStatus.Size = new Size(18, 21);
            pbStatus.TabIndex = 23;
            pbStatus.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 5);
            label5.Name = "label5";
            label5.Size = new Size(82, 20);
            label5.TabIndex = 24;
            label5.Text = "No conflict";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.warning;
            pictureBox1.Location = new Point(0, 5);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(18, 21);
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cbBoxPrefix);
            panel1.Controls.Add(cbBoxSuffix);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(14, 309);
            panel1.Name = "panel1";
            panel1.Size = new Size(493, 74);
            panel1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(button3);
            panel2.Location = new Point(15, 151);
            panel2.Name = "panel2";
            panel2.Size = new Size(271, 64);
            panel2.TabIndex = 27;
            // 
            // panel3
            // 
            panel3.Controls.Add(label5);
            panel3.Controls.Add(pbStatus);
            panel3.Location = new Point(87, 168);
            panel3.Name = "panel3";
            panel3.Size = new Size(125, 66);
            panel3.TabIndex = 28;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(312, 11);
            label8.Name = "label8";
            label8.Size = new Size(76, 20);
            label8.TabIndex = 19;
            label8.Text = "ByteOrder";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(67, 24);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Ex : 01";
            textBox1.Size = new Size(109, 27);
            textBox1.TabIndex = 29;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(67, 75);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Ex : 0123";
            textBox2.Size = new Size(109, 27);
            textBox2.TabIndex = 31;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(10, 27);
            label9.Name = "label9";
            label9.Size = new Size(56, 20);
            label9.TabIndex = 30;
            label9.Text = "2 Bytes";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(67, 127);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Ex : 01234567";
            textBox3.Size = new Size(109, 27);
            textBox3.TabIndex = 33;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 78);
            label10.Name = "label10";
            label10.Size = new Size(56, 20);
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
            panel4.Location = new Point(312, 35);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(195, 179);
            panel4.TabIndex = 34;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 130);
            label6.Name = "label6";
            label6.Size = new Size(56, 20);
            label6.TabIndex = 35;
            label6.Text = "8 Bytes";
            // 
            // panel5
            // 
            panel5.Controls.Add(label7);
            panel5.Controls.Add(textBox4);
            panel5.Location = new Point(14, 240);
            panel5.Name = "panel5";
            panel5.Size = new Size(493, 45);
            panel5.TabIndex = 35;
            panel5.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 15);
            label7.Name = "label7";
            label7.Size = new Size(185, 20);
            label7.TabIndex = 1;
            label7.Text = "Start element adress gap : ";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(194, 12);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Ex : 1";
            textBox4.Size = new Size(41, 27);
            textBox4.TabIndex = 0;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // FrmImportFile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(532, 440);
            Controls.Add(panel5);
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
            Name = "FrmImportFile";
            Text = "Import channels";
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
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
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
        private Panel panel5;
        private TextBox textBox4;
    }
}