namespace OCRPDF
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.pbOriginal = new System.Windows.Forms.PictureBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.pbCrop = new System.Windows.Forms.PictureBox();
            this.btnPage = new System.Windows.Forms.Button();
            this.autoCalculate = new System.Windows.Forms.CheckBox();
            this.txtExtracted = new System.Windows.Forms.RichTextBox();
            this.chkLocked = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(12, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 9;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(93, 4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(168, 20);
            this.txtPath.TabIndex = 8;
            // 
            // pbOriginal
            // 
            this.pbOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOriginal.Location = new System.Drawing.Point(9, 32);
            this.pbOriginal.Margin = new System.Windows.Forms.Padding(0);
            this.pbOriginal.Name = "pbOriginal";
            this.pbOriginal.Size = new System.Drawing.Size(250, 250);
            this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOriginal.TabIndex = 6;
            this.pbOriginal.TabStop = false;
            this.pbOriginal.Paint += new System.Windows.Forms.PaintEventHandler(this.pbOriginal_Paint);
            this.pbOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbOriginal_MouseDown);
            this.pbOriginal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbOriginal_MouseMove);
            this.pbOriginal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbOriginal_MouseUp);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(688, 4);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 26);
            this.btnCalculate.TabIndex = 5;
            this.btnCalculate.Text = "Calculate!";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // pbCrop
            // 
            this.pbCrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCrop.Location = new System.Drawing.Point(267, 32);
            this.pbCrop.Margin = new System.Windows.Forms.Padding(0);
            this.pbCrop.Name = "pbCrop";
            this.pbCrop.Size = new System.Drawing.Size(500, 500);
            this.pbCrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCrop.TabIndex = 10;
            this.pbCrop.TabStop = false;
            this.pbCrop.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCrop_Paint);
            this.pbCrop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCrop_MouseDown);
            this.pbCrop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCrop_MouseMove);
            this.pbCrop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCrop_MouseUp);
            // 
            // btnPage
            // 
            this.btnPage.Location = new System.Drawing.Point(267, 1);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(75, 26);
            this.btnPage.TabIndex = 11;
            this.btnPage.Text = "Next Page";
            this.btnPage.UseVisualStyleBackColor = true;
            this.btnPage.Click += new System.EventHandler(this.btnPage_Click);
            // 
            // autoCalculate
            // 
            this.autoCalculate.AutoSize = true;
            this.autoCalculate.Checked = true;
            this.autoCalculate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoCalculate.Location = new System.Drawing.Point(587, 8);
            this.autoCalculate.Name = "autoCalculate";
            this.autoCalculate.Size = new System.Drawing.Size(95, 17);
            this.autoCalculate.TabIndex = 12;
            this.autoCalculate.Text = "Auto Calculate";
            this.autoCalculate.UseVisualStyleBackColor = true;
            // 
            // txtExtracted
            // 
            this.txtExtracted.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExtracted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExtracted.Location = new System.Drawing.Point(0, 0);
            this.txtExtracted.Name = "txtExtracted";
            this.txtExtracted.Size = new System.Drawing.Size(248, 222);
            this.txtExtracted.TabIndex = 13;
            this.txtExtracted.Text = "";
            this.txtExtracted.WordWrap = false;
            this.txtExtracted.TextChanged += new System.EventHandler(this.txtExtracted_TextChanged);
            // 
            // chkLocked
            // 
            this.chkLocked.AutoSize = true;
            this.chkLocked.Location = new System.Drawing.Point(209, 515);
            this.chkLocked.Name = "chkLocked";
            this.chkLocked.Size = new System.Drawing.Size(50, 17);
            this.chkLocked.TabIndex = 14;
            this.chkLocked.Text = "Lock";
            this.chkLocked.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 518);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtExtracted);
            this.panel1.Location = new System.Drawing.Point(9, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 224);
            this.panel1.TabIndex = 16;
            // 
            // txtFlags
            // 
            this.txtFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlags.ForeColor = System.Drawing.Color.DarkRed;
            this.txtFlags.Location = new System.Drawing.Point(151, 548);
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(612, 13);
            this.txtFlags.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "FLAGS:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(769, 574);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFlags);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkLocked);
            this.Controls.Add(this.autoCalculate);
            this.Controls.Add(this.pbCrop);
            this.Controls.Add(this.btnPage);
            this.Controls.Add(this.pbOriginal);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EurekaFacts PDF OCR";
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtPath;
        protected System.Windows.Forms.PictureBox pbOriginal;
        private System.Windows.Forms.Button btnCalculate;
        protected System.Windows.Forms.PictureBox pbCrop;
        private System.Windows.Forms.Button btnPage;
        private System.Windows.Forms.CheckBox autoCalculate;
        private System.Windows.Forms.RichTextBox txtExtracted;
        private System.Windows.Forms.CheckBox chkLocked;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.Label label1;
    }
}

