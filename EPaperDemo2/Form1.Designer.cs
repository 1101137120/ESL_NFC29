namespace EPaperDemo2
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.UpCOM = new System.Windows.Forms.Button();
            this.Connect_COM_Button = new System.Windows.Forms.Button();
            this.ConnectStatus = new System.Windows.Forms.Label();
            this.cbbComPort = new System.Windows.Forms.ComboBox();
            this.ReadUID = new System.Windows.Forms.Button();
            this.cbCOMPort = new System.Windows.Forms.ComboBox();
            this.texMessageBox = new System.Windows.Forms.TextBox();
            this.HINKWriteButton = new System.Windows.Forms.Button();
            this.HINKViewerButton = new System.Windows.Forms.Button();
            this.panel29 = new System.Windows.Forms.Panel();
            this.pictureBox_29 = new System.Windows.Forms.PictureBox();
            this.continue29 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_29)).BeginInit();
            this.SuspendLayout();
            // 
            // UpCOM
            // 
            this.UpCOM.Location = new System.Drawing.Point(395, 12);
            this.UpCOM.Name = "UpCOM";
            this.UpCOM.Size = new System.Drawing.Size(75, 23);
            this.UpCOM.TabIndex = 95;
            this.UpCOM.Text = "更新";
            this.UpCOM.UseVisualStyleBackColor = true;
            this.UpCOM.Click += new System.EventHandler(this.UpCOM_Click);
            // 
            // Connect_COM_Button
            // 
            this.Connect_COM_Button.Location = new System.Drawing.Point(312, 12);
            this.Connect_COM_Button.Name = "Connect_COM_Button";
            this.Connect_COM_Button.Size = new System.Drawing.Size(75, 23);
            this.Connect_COM_Button.TabIndex = 94;
            this.Connect_COM_Button.Text = "serial連線";
            this.Connect_COM_Button.UseVisualStyleBackColor = true;
            this.Connect_COM_Button.Click += new System.EventHandler(this.Connect_COM_Button_Click);
            // 
            // ConnectStatus
            // 
            this.ConnectStatus.AutoSize = true;
            this.ConnectStatus.Location = new System.Drawing.Point(227, 18);
            this.ConnectStatus.Name = "ConnectStatus";
            this.ConnectStatus.Size = new System.Drawing.Size(71, 12);
            this.ConnectStatus.TabIndex = 93;
            this.ConnectStatus.Text = "Connect Fail !";
            // 
            // cbbComPort
            // 
            this.cbbComPort.FormattingEnabled = true;
            this.cbbComPort.Location = new System.Drawing.Point(9, 13);
            this.cbbComPort.Name = "cbbComPort";
            this.cbbComPort.Size = new System.Drawing.Size(103, 20);
            this.cbbComPort.TabIndex = 92;
            // 
            // ReadUID
            // 
            this.ReadUID.Location = new System.Drawing.Point(8, 39);
            this.ReadUID.Name = "ReadUID";
            this.ReadUID.Size = new System.Drawing.Size(120, 23);
            this.ReadUID.TabIndex = 96;
            this.ReadUID.Text = "讀取UID";
            this.ReadUID.UseVisualStyleBackColor = true;
            this.ReadUID.Click += new System.EventHandler(this.ReadUID_Click);
            // 
            // cbCOMPort
            // 
            this.cbCOMPort.FormattingEnabled = true;
            this.cbCOMPort.Items.AddRange(new object[] {
            "115200"});
            this.cbCOMPort.Location = new System.Drawing.Point(127, 14);
            this.cbCOMPort.Name = "cbCOMPort";
            this.cbCOMPort.Size = new System.Drawing.Size(85, 20);
            this.cbCOMPort.TabIndex = 101;
            this.cbCOMPort.Text = "115200";
            // 
            // texMessageBox
            // 
            this.texMessageBox.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.texMessageBox.Location = new System.Drawing.Point(9, 315);
            this.texMessageBox.Multiline = true;
            this.texMessageBox.Name = "texMessageBox";
            this.texMessageBox.Size = new System.Drawing.Size(637, 142);
            this.texMessageBox.TabIndex = 102;
            // 
            // HINKWriteButton
            // 
            this.HINKWriteButton.Location = new System.Drawing.Point(375, 286);
            this.HINKWriteButton.Name = "HINKWriteButton";
            this.HINKWriteButton.Size = new System.Drawing.Size(75, 23);
            this.HINKWriteButton.TabIndex = 107;
            this.HINKWriteButton.Text = "2.9吋寫入";
            this.HINKWriteButton.UseVisualStyleBackColor = true;
            this.HINKWriteButton.Click += new System.EventHandler(this.HINKWriteButton_Click);
            // 
            // HINKViewerButton
            // 
            this.HINKViewerButton.Location = new System.Drawing.Point(53, 286);
            this.HINKViewerButton.Name = "HINKViewerButton";
            this.HINKViewerButton.Size = new System.Drawing.Size(75, 23);
            this.HINKViewerButton.TabIndex = 106;
            this.HINKViewerButton.Text = "2.9吋預覽";
            this.HINKViewerButton.UseVisualStyleBackColor = true;
            this.HINKViewerButton.Click += new System.EventHandler(this.HINKViewerButton_Click);
            // 
            // panel29
            // 
            this.panel29.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel29.Location = new System.Drawing.Point(22, 152);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(296, 128);
            this.panel29.TabIndex = 108;
            // 
            // pictureBox_29
            // 
            this.pictureBox_29.Location = new System.Drawing.Point(350, 152);
            this.pictureBox_29.Name = "pictureBox_29";
            this.pictureBox_29.Size = new System.Drawing.Size(296, 128);
            this.pictureBox_29.TabIndex = 109;
            this.pictureBox_29.TabStop = false;
            // 
            // continue29
            // 
            this.continue29.Location = new System.Drawing.Point(476, 286);
            this.continue29.Name = "continue29";
            this.continue29.Size = new System.Drawing.Size(83, 23);
            this.continue29.TabIndex = 110;
            this.continue29.Text = "2.9連續寫入";
            this.continue29.UseVisualStyleBackColor = true;
            this.continue29.Click += new System.EventHandler(this.continue29_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(322, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 111;
            this.label2.Text = "00000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(322, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 112;
            this.label4.Text = "00000";
            // 
            // txtImport
            // 
            this.txtImport.Location = new System.Drawing.Point(476, 12);
            this.txtImport.Name = "txtImport";
            this.txtImport.Size = new System.Drawing.Size(75, 23);
            this.txtImport.TabIndex = 113;
            this.txtImport.Text = "匯入txt";
            this.txtImport.UseVisualStyleBackColor = true;
            this.txtImport.Click += new System.EventHandler(this.txtImport_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(659, 462);
            this.Controls.Add(this.txtImport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.continue29);
            this.Controls.Add(this.pictureBox_29);
            this.Controls.Add(this.panel29);
            this.Controls.Add(this.HINKWriteButton);
            this.Controls.Add(this.HINKViewerButton);
            this.Controls.Add(this.texMessageBox);
            this.Controls.Add(this.cbCOMPort);
            this.Controls.Add(this.ReadUID);
            this.Controls.Add(this.UpCOM);
            this.Controls.Add(this.Connect_COM_Button);
            this.Controls.Add(this.ConnectStatus);
            this.Controls.Add(this.cbbComPort);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_29)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button UpCOM;
        private System.Windows.Forms.Button Connect_COM_Button;
        private System.Windows.Forms.Label ConnectStatus;
        private System.Windows.Forms.ComboBox cbbComPort;
        private System.Windows.Forms.Button ReadUID;
        private System.Windows.Forms.ComboBox cbCOMPort;
        private System.Windows.Forms.TextBox texMessageBox;
        private System.Windows.Forms.Button HINKWriteButton;
        private System.Windows.Forms.Button HINKViewerButton;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.PictureBox pictureBox_29;
        private System.Windows.Forms.Button continue29;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button txtImport;
    }
}

