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
    internal delegate void SelectServerHandler(VpnServerProfile profile);

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
        private List<Server> ParseServerList(string json)
        {
            var servers = JsonConvert.DeserializeObject<List<Server>>(json);
            if (servers == null)
                throw new JsonSerializationException("The server list is empty or has an invalid format.");

            return servers
                .Where(x => x != null && x.PORT == 443 && !string.IsNullOrWhiteSpace(x.HOSTNAME))
                .ToList();
        }

        private async Task GetServerListAsync()
        {
            prgLoading.Visible = true;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(15);
                    var json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/FreeSSTP/server-list/main/Records.json");
                    var remoteServers = ParseServerList(json);

                    if (remoteServers.Count == 0)
                        throw new JsonSerializationException("The remote server list contains no usable SSTP servers.");

                    list = remoteServers;

                    // A cache write failure should not prevent use of a valid remote list.
                    try
                    {
                        File.WriteAllText(@"Records.json", JsonConvert.SerializeObject(list));
                    }
                    catch (IOException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
            }
            catch (Exception remoteException)
            {
                try
                {
                    if (!File.Exists(@"Records.json"))
                        throw new FileNotFoundException("No local server-list cache is available.");

                    string cachedJson = File.ReadAllText(@"Records.json");
                    list = ParseServerList(cachedJson);

                    if (list.Count == 0)
                        throw new JsonSerializationException("The cached server list contains no usable SSTP servers.");
                }
                catch (Exception cacheException)
                {
                    list = new List<Server>();
                    MessageBox.Show(
                        "Unable to load the server list from the internet or local cache.\n\n" +
                        "Remote error: " + remoteException.Message + "\n" +
                        "Cache error: " + cacheException.Message,
                        "Server list unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            finally
            {
                fillGrid(list);
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

        private void btnManageManualServers_Click(object sender, EventArgs e)
        {
            using (var form = new frmManualServers())
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.SelectedServer != null)
                {
                    var manualServer = form.SelectedServer;
                    string displayName = string.IsNullOrWhiteSpace(manualServer.Name)
                        ? manualServer.Hostname
                        : manualServer.Name;

                    if (MessageBox.Show(
                        "Use this manual VPN server?\n\n" + displayName + " (" +
                        manualServer.Hostname + ":" + manualServer.Port + ")",
                        "Select manual server",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        OnSelectServer?.Invoke(VpnServerProfile.FromManualServer(manualServer));
                        Close();
                    }
                }
            }
        }

        private void btnSelectServer_Click(object sender, EventArgs e)
        {
            Server server = (Server)(sender as Button).Tag;

            if (MessageBox.Show($"Change the VPN connection to \n{server.LOCATION} ::: \"{server.HOSTNAME}\"?", "Change the VPN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OnSelectServer?.Invoke(VpnServerProfile.FromFreeServer(server));
                this.Close();
            }
        }
    }
}
