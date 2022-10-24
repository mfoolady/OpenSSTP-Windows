using System.Windows.Forms;

namespace OpenSSTP
{
    partial class frmServerList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.LOCATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HOSTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SESSIONS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINE_QUALITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCORE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LOCATION,
            this.HOSTNAME,
            this.PING,
            this.UPTIME,
            this.SESSIONS,
            this.LINE_QUALITY,
            this.PORT,
            this.SCORE,
            this.FLAG});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(634, 362);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            // 
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(229, 180);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(182, 20);
            this.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgLoading.TabIndex = 1;
            // 
            // LOCATION
            // 
            this.LOCATION.DataPropertyName = "LOCATION";
            this.LOCATION.HeaderText = "Location";
            this.LOCATION.Name = "LOCATION";
            this.LOCATION.ReadOnly = true;
            // 
            // HOSTNAME
            // 
            this.HOSTNAME.DataPropertyName = "HOSTNAME";
            this.HOSTNAME.FillWeight = 180F;
            this.HOSTNAME.HeaderText = "Host";
            this.HOSTNAME.Name = "HOSTNAME";
            this.HOSTNAME.ReadOnly = true;
            // 
            // PING
            // 
            this.PING.DataPropertyName = "PING";
            this.PING.FillWeight = 50F;
            this.PING.HeaderText = "Ping";
            this.PING.Name = "PING";
            this.PING.ReadOnly = true;
            // 
            // UPTIME
            // 
            this.UPTIME.DataPropertyName = "UPTIME";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UPTIME.DefaultCellStyle = dataGridViewCellStyle7;
            this.UPTIME.FillWeight = 80F;
            this.UPTIME.HeaderText = "UpTime";
            this.UPTIME.Name = "UPTIME";
            this.UPTIME.ReadOnly = true;
            // 
            // SESSIONS
            // 
            this.SESSIONS.DataPropertyName = "SESSIONS";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SESSIONS.DefaultCellStyle = dataGridViewCellStyle8;
            this.SESSIONS.FillWeight = 60F;
            this.SESSIONS.HeaderText = "Sessions";
            this.SESSIONS.Name = "SESSIONS";
            this.SESSIONS.ReadOnly = true;
            // 
            // LINE_QUALITY
            // 
            this.LINE_QUALITY.DataPropertyName = "LINE_QUALITY";
            this.LINE_QUALITY.FillWeight = 80F;
            this.LINE_QUALITY.HeaderText = "LineQuality";
            this.LINE_QUALITY.Name = "LINE_QUALITY";
            this.LINE_QUALITY.ReadOnly = true;
            // 
            // PORT
            // 
            this.PORT.DataPropertyName = "PORT";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PORT.DefaultCellStyle = dataGridViewCellStyle9;
            this.PORT.FillWeight = 60F;
            this.PORT.HeaderText = "Port";
            this.PORT.Name = "PORT";
            this.PORT.ReadOnly = true;
            this.PORT.Visible = false;
            // 
            // SCORE
            // 
            this.SCORE.DataPropertyName = "SCORE";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SCORE.DefaultCellStyle = dataGridViewCellStyle10;
            this.SCORE.HeaderText = "Score";
            this.SCORE.Name = "SCORE";
            this.SCORE.ReadOnly = true;
            this.SCORE.Visible = false;
            // 
            // FLAG
            // 
            this.FLAG.DataPropertyName = "FLAG";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FLAG.DefaultCellStyle = dataGridViewCellStyle11;
            this.FLAG.HeaderText = "Flag";
            this.FLAG.Name = "FLAG";
            this.FLAG.ReadOnly = true;
            this.FLAG.Visible = false;
            // 
            // frmServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 362);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmServerList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Free Server List ::: double click on each row to select the server.";
            this.Load += new System.EventHandler(this.frmServerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView;
        private ProgressBar prgLoading;
        private DataGridViewTextBoxColumn LOCATION;
        private DataGridViewTextBoxColumn HOSTNAME;
        private DataGridViewTextBoxColumn PING;
        private DataGridViewTextBoxColumn UPTIME;
        private DataGridViewTextBoxColumn SESSIONS;
        private DataGridViewTextBoxColumn LINE_QUALITY;
        private DataGridViewTextBoxColumn PORT;
        private DataGridViewTextBoxColumn SCORE;
        private DataGridViewTextBoxColumn FLAG;
    }
}