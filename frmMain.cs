using DotRas;
using Newtonsoft.Json;
using OpenSSTP.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSSTP
{
    public partial class frmMain : Form
    {
        private int CURRENT_VERSION = 100;

        public const string ConnectionName = "OpenSSTP (Free Server)";
        private RasHandle handle;
        RasEntry entry;
        RasPhoneBook rasUsersPhoneBook = new RasPhoneBook();
        RasDialer Dialer = new RasDialer();
        private BAL.Server server;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (IsConnected())
            {
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.ON;
                lblStatus.Text = "VPN Connection already running";
            }
            else
            {
                Disconnect();
            }

            this.server = BAL.Server.LoadConfigFromFile();

            if (server != null)
            {
                this.lblLocation.Text = server.LOCATION;
                this.lblHost.Text = server.HOSTNAME;

                picFlag.ImageLocation = server.FLAG;
            }

            _ = GetServerListAsync();
        }

        private async Task GetServerListAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/FreeSSTP/server-list/main/win_ver.json");
                    WinVersion ver = JsonConvert.DeserializeObject<WinVersion>(json);
                    if(ver != null && ver.version_code > CURRENT_VERSION)
                    {
                        if (MessageBox.Show($"The new version is available!\n\nDownload now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            Process.Start(new ProcessStartInfo(ver.url) { UseShellExecute = true });
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSelectServer_Click(object sender, EventArgs e)
        {
            showFormServer();
        }

        private void showFormServer()
        {
            frmServerList frm = new frmServerList();
            frm.OnSelectServer += Frm_OnSelectServer;
            frm.ShowDialog();
        }

        private void Frm_OnSelectServer(BAL.Server server)
        {
            this.server = server;
            this.lblLocation.Text = server.LOCATION;
            this.lblHost.Text = server.HOSTNAME;
            this.picFlag.ImageLocation = server.FLAG;

            server.SaveToFile();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected() && MessageBox.Show($"The VPN will be disconnected!\n\nAre you sure?", "Disconnect the VPN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
                Disconnect();
        }

        public bool IsConnected()
        {
            bool isConnected = false;
            foreach (RasConnection rasCon in RasConnection.GetActiveConnections())
                if (rasCon.EntryName == ConnectionName)
                    isConnected = true;
            return isConnected;
        }

        public void Connect()
        {
            if (server == null)
                showFormServer();
            else if (!IsConnected())
            {
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.GRAY;

                string targetPath1 = Path.Combine(
                   Environment.GetFolderPath(
                       Environment.SpecialFolder.ApplicationData)) + "\\Microsoft\\Network\\Connections\\Pbk";
                string targetPath = Path.Combine(
                       Environment.GetFolderPath(
                           Environment.SpecialFolder.ApplicationData)) + "\\Microsoft\\Network\\Connections\\Pbk";

                bool checkUser = false;

                if (targetPath.Contains("Roaming"))
                {
                    rasUsersPhoneBook.Open(targetPath + "\\rasphone.pbk");
                    checkUser = true;
                }
                else
                    rasUsersPhoneBook.Open();


                entry = RasEntry.CreateVpnEntry(ConnectionName, server.HOSTNAME, RasVpnStrategy.SstpOnly,
                    RasDevice.GetDevices().FirstOrDefault(d => d.Name.Contains("SSTP")));
                entry.Options.CacheCredentials = true;
                entry.Options.Internet = true;

                if (!this.rasUsersPhoneBook.Entries.Contains(entry.Name))
                    this.rasUsersPhoneBook.Entries.Add(entry);

                this.lblStatus.Text = "";
                this.Dialer.EntryName = ConnectionName;

                if (checkUser == true)
                    this.Dialer.PhoneBookPath = targetPath + "\\rasphone.pbk";
                else
                    this.Dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

                try
                {
                    // Set the credentials the dialer should use.
                    this.Dialer.Credentials = new NetworkCredential("vpn", "vpn");
                    Dialer.AllowUseStoredCredentials = true;

                    Dialer.Error += Dialer_Error;
                    Dialer.StateChanged += Dialer_StateChanged1;
                    Dialer.DialCompleted += Dialer_DialCompleted;

                    this.handle = this.Dialer.DialAsync();

                    this.picConnection.Image = global::OpenSSTP.Properties.Resources.GRAY;
                }
                catch (Exception ex)
                {
                    this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
                    this.lblStatus.Text = "Error: " + ex.ToString();
                }
            }
        }

        private void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.lblStatus.Text = "Cancelled!";
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            }
            else if (e.TimedOut)
            {
                this.lblStatus.Text = "Connection attempt timed out!";
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            }
            else if (e.Error != null)
            {
                this.lblStatus.Text = e.Error.ToString();
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            }
            else if (e.Connected)
            {
                this.lblStatus.Text = "Connection successful!";
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.ON;
            }

            if (!e.Connected)
            {
                this.lblStatus.Text = "Disconnected!";
                this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            }
        }

        private void Dialer_StateChanged1(object sender, StateChangedEventArgs e)
        {
            this.lblStatus.Invoke(new Action(() => this.lblStatus.Text = e.State.ToString()));
        }

        private void Dialer_Error(object sender, ErrorEventArgs e)
        {
            RasConnection connection = RasConnection.GetActiveConnections()[0];
            this.lblStatus.Text = connection.GetConnectionStatus().ToString();
            this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
            MessageBox.Show(e.GetException().Message);
        }

        public void Disconnect()
        {
            this.picConnection.Image = global::OpenSSTP.Properties.Resources.GRAY;

            if (this.Dialer.IsBusy)
            {
                // The connection attempt has not been completed, cancel the attempt.
                this.Dialer.DialAsyncCancel();
            }
            else if (this.handle != null)
            {
                RasConnection connection = RasConnection.GetActiveConnectionByHandle(this.handle);

                if (connection != null)
                {
                    // The connection has been found, disconnect it.
                    if (connection.EntryName == ConnectionName)
                    {
                        connection.HangUp();
                        this.rasUsersPhoneBook.Entries.Remove(connection.EntryName);
                        rasUsersPhoneBook.Dispose();
                    }
                }
            }
            this.lblStatus.Text = "";
            this.lblStatus.Text = "Disconnected!";
            this.picConnection.Image = global::OpenSSTP.Properties.Resources.OFF;
        }

        private void picConnection_Click(object sender, EventArgs e)
        {
            if (IsConnected() || this.Dialer.IsBusy)
                Disconnect();
            else
                Connect();
        }


    }
}
