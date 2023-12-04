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
            ((System.ComponentModel.ISupportInitialize)pbStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
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
            label2.Location = new Point(0, 52);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 17;
            label2.Text = "Suffix";
            // 
            // cbBoxSuffix
            // 
            cbBoxSuffix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxSuffix.FormattingEnabled = true;
            cbBoxSuffix.Location = new Point(0, 70);
            cbBoxSuffix.Margin = new Padding(3, 2, 3, 2);
            cbBoxSuffix.Name = "cbBoxSuffix";
            cbBoxSuffix.Size = new Size(238, 23);
            cbBoxSuffix.TabIndex = 16;
            // 
            // cbBoxPrefix
            // 
            cbBoxPrefix.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxPrefix.FormattingEnabled = true;
            cbBoxPrefix.Location = new Point(0, 20);
            cbBoxPrefix.Margin = new Padding(3, 2, 3, 2);
            cbBoxPrefix.Name = "cbBoxPrefix";
            cbBoxPrefix.Size = new Size(238, 23);
            cbBoxPrefix.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(146, 320);
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
            button2.Location = new Point(12, 320);
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
            button3.Location = new Point(0, 21);
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
            panel1.Location = new Point(10, 118);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(248, 94);
            panel1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(11, 236);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(237, 48);
            panel2.TabIndex = 27;
            // 
            // panel3
            // 
            panel3.Controls.Add(label5);
            panel3.Controls.Add(pbStatus);
            panel3.Location = new Point(12, 246);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(237, 27);
            panel3.TabIndex = 28;
            // 
            // FrmImportFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 352);
            Controls.Add(panel3);
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
            Text = "FrmImportFile";
            ((System.ComponentModel.ISupportInitialize)pbStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
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
    }
}