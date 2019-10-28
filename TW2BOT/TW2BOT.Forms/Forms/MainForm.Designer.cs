namespace TW2BOT.Forms
{
    partial class MainForm
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
            this.btn_toggleFarmingService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_farmingStatus = new System.Windows.Forms.Label();
            this.listView_logging = new System.Windows.Forms.ListView();
            this.btn_connect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_closeWindows = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_toggleFarmingService
            // 
            this.btn_toggleFarmingService.Location = new System.Drawing.Point(16, 174);
            this.btn_toggleFarmingService.Name = "btn_toggleFarmingService";
            this.btn_toggleFarmingService.Size = new System.Drawing.Size(119, 49);
            this.btn_toggleFarmingService.TabIndex = 0;
            this.btn_toggleFarmingService.Text = "button1";
            this.btn_toggleFarmingService.UseVisualStyleBackColor = true;
            this.btn_toggleFarmingService.Click += new System.EventHandler(this.Btn_toggleFarmingService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Farming service:";
            // 
            // lbl_farmingStatus
            // 
            this.lbl_farmingStatus.AutoSize = true;
            this.lbl_farmingStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_farmingStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_farmingStatus.Location = new System.Drawing.Point(103, 158);
            this.lbl_farmingStatus.Name = "lbl_farmingStatus";
            this.lbl_farmingStatus.Size = new System.Drawing.Size(0, 13);
            this.lbl_farmingStatus.TabIndex = 2;
            this.lbl_farmingStatus.Click += new System.EventHandler(this.Label2_Click);
            // 
            // listView_logging
            // 
            this.listView_logging.HideSelection = false;
            this.listView_logging.Location = new System.Drawing.Point(400, 12);
            this.listView_logging.Name = "listView_logging";
            this.listView_logging.Size = new System.Drawing.Size(631, 426);
            this.listView_logging.TabIndex = 3;
            this.listView_logging.UseCompatibleStateImageBehavior = false;
            this.listView_logging.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(16, 12);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(119, 49);
            this.btn_connect.TabIndex = 4;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "AddDumpLog";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btn_closeWindows
            // 
            this.btn_closeWindows.Location = new System.Drawing.Point(285, 415);
            this.btn_closeWindows.Name = "btn_closeWindows";
            this.btn_closeWindows.Size = new System.Drawing.Size(109, 23);
            this.btn_closeWindows.TabIndex = 6;
            this.btn_closeWindows.Text = "Close windows";
            this.btn_closeWindows.UseVisualStyleBackColor = true;
            this.btn_closeWindows.Click += new System.EventHandler(this.Btn_closeWindows_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 450);
            this.Controls.Add(this.btn_closeWindows);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.listView_logging);
            this.Controls.Add(this.lbl_farmingStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_toggleFarmingService);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_toggleFarmingService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_farmingStatus;
        private System.Windows.Forms.ListView listView_logging;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_closeWindows;
    }
}