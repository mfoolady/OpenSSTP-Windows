using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSSTP.BAL
{
    internal partial class ServerItemControl : UserControl
    {
        private Server server { get; set; }

        public ServerItemControl()
        {
            InitializeComponent();
        }

        public ServerItemControl(Server server) : this()
        {
            this.server = server;
            this.btnSelectServer.Tag = server;

            this.Host.Text = server.HOSTNAME;
            this.Location.Text = server.LOCATION;
            this.Ping.Text = server.PING.ToString();
            this.Uptime.Text = server.UPTIME;
            this.Session.Text = server.SESSIONS.ToString();
            this.Speed.Text = server.LINE_QUALITY;
            this.Score.Text = server.SCORE.ToString();
            this.Flag.ImageLocation = server.FLAG;           
        }

    }
}
