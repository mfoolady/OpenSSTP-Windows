namespace OpenSSTP
{
    partial class frmListCustom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListCustom));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.btnManageManualServers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 44);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(434, 467);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnManageManualServers
            //
            this.btnManageManualServers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManageManualServers.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnManageManualServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageManualServers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnManageManualServers.Location = new System.Drawing.Point(246, 8);
            this.btnManageManualServers.Name = "btnManageManualServers";
            this.btnManageManualServers.Size = new System.Drawing.Size(180, 28);
            this.btnManageManualServers.TabIndex = 3;
            this.btnManageManualServers.Text = "Manage Manual Servers";
            this.btnManageManualServers.UseVisualStyleBackColor = false;
            this.btnManageManualServers.Click += new System.EventHandler(this.btnManageManualServers_Click);
            //
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(125, 215);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(200, 20);
            this.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgLoading.TabIndex = 2;
            // 
            // frmListCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(434, 511);
            this.Controls.Add(this.btnManageManualServers);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmListCustom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Free Server List";
            this.Load += new System.EventHandler(this.frmListCustom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.Button btnManageManualServers;
    }
}