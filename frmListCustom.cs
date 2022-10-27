using Newtonsoft.Json;
using OpenSSTP.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;

namespace OpenSSTP
{
    internal delegate void SelectServerHandler(Server server);

    public partial class frmListCustom : Form
    {
        internal event SelectServerHandler OnSelectServer;

        private int _previousIndex;
        private bool _sortDirection;
        List<Server> list = new List<Server>();

        public frmListCustom()
        {
            InitializeComponent();
        }

        private void frmListCustom_Load(object sender, EventArgs e)
        {
            _ = GetServerListAsync();
        }
        private async Task GetServerListAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    prgLoading.Visible = true;
                    var json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/FreeSSTP/server-list/main/Records.json");
                    list = JsonConvert.DeserializeObject<List<Server>>(json).Where(x => x.PORT == 443).ToList();
                    File.WriteAllText(@"Records.json", JsonConvert.SerializeObject(list));
                    fillGrid(list);
                }
            }
            catch
            {
                if (File.Exists(@"Records.json"))
                {
                    string json = File.ReadAllText(@"Records.json");
                    list = JsonConvert.DeserializeObject<List<Server>>(json).ToList();
                    fillGrid(list);
                }
            }
            finally
            {
                prgLoading.Visible = false;
            }
        }

        private void fillGrid(List<Server> list)
        {
            this.flowLayoutPanel1.Visible = false;
            this.flowLayoutPanel1.Controls.Clear();

            foreach (var item in list)
            {  
                ServerItemControl c = new ServerItemControl(item);
                c.btnSelectServer.Click += btnSelectServer_Click;
                this.flowLayoutPanel1.Controls.Add(c);
            }

            this.prgLoading.Visible = false;
            this.flowLayoutPanel1.Visible = true;
        }

        private void btnSelectServer_Click(object sender, EventArgs e)
        {
            Server server = (Server)(sender as Button).Tag;

            if (MessageBox.Show($"Change the VPN connection to \n{server.LOCATION} ::: \"{server.HOSTNAME}\"?", "Change the VPN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OnSelectServer?.Invoke(server);
                this.Close();
            }
        }
    }
}
