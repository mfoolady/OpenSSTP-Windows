using System.Windows.Forms;

namespace OpenSSTP
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
            this.picConnection = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picFlag = new System.Windows.Forms.PictureBox();
            this.btnSelectServer = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picConnection)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFlag)).BeginInit();
            this.SuspendLayout();
            // 
            // picConnection
            // 
            this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            this.picConnection.Location = new System.Drawing.Point(84, 71);
            this.picConnection.Name = "picConnection";
            this.picConnection.Size = new System.Drawing.Size(171, 173);
            this.picConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picConnection.TabIndex = 0;
            this.picConnection.TabStop = false;
            this.picConnection.Click += new System.EventHandler(this.picConnection_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 251);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.panel1.Size = new System.Drawing.Size(340, 52);
            this.panel1.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblStatus.Location = new System.Drawing.Point(9, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(322, 34);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picFlag);
            this.panel2.Controls.Add(this.btnSelectServer);
            this.panel2.Controls.Add(this.lblHost);
            this.panel2.Controls.Add(this.lblLocation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 64);
            this.panel2.TabIndex = 2;
            // 
            // picFlag
            // 
            this.picFlag.BackColor = System.Drawing.Color.Transparent;
            this.picFlag.Location = new System.Drawing.Point(10, 3);
            this.picFlag.Name = "picFlag";
            this.picFlag.Size = new System.Drawing.Size(33, 58);
            this.picFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFlag.TabIndex = 7;
            this.picFlag.TabStop = false;
            // 
            // btnSelectServer
            // 
            this.btnSelectServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectServer.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSelectServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelectServer.ForeColor = System.Drawing.Color.Silver;
            this.btnSelectServer.Location = new System.Drawing.Point(257, 14);
            this.btnSelectServer.Name = "btnSelectServer";
            this.btnSelectServer.Padding = new System.Windows.Forms.Padding(3);
            this.btnSelectServer.Size = new System.Drawing.Size(72, 36);
            this.btnSelectServer.TabIndex = 6;
            this.btnSelectServer.Text = "Servers";
            this.btnSelectServer.UseVisualStyleBackColor = false;
            this.btnSelectServer.Click += new System.EventHandler(this.btnSelectServer_Click);
            // 
            // lblHost
            // 
            this.lblHost.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHost.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHost.Location = new System.Drawing.Point(48, 36);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(210, 15);
            this.lblHost.TabIndex = 4;
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLocation.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblLocation.Location = new System.Drawing.Point(48, 14);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(210, 15);
            this.lblLocation.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(340, 303);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picConnection);
            this.ForeColor = System.Drawing.Color.DarkGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open SSTP (Free Server)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picConnection)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFlag)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox picConnection;
        private Panel panel1;
        private Panel panel2;
        private Button btnSelectServer;
        private Label lblHost;
        private Label lblLocation;
        private Label lblStatus;
        private PictureBox picFlag;
    }
}