using OpenSSTP.BAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSSTP
{
    internal sealed class frmManualServers : Form
    {
        private readonly ListBox lstServers = new ListBox();
        private readonly TextBox txtName = new TextBox();
        private readonly TextBox txtHost = new TextBox();
        private readonly NumericUpDown numPort = new NumericUpDown();
        private readonly TextBox txtUsername = new TextBox();
        private readonly TextBox txtPassword = new TextBox();
        private readonly Button btnSave = new Button();
        private readonly Button btnNew = new Button();
        private readonly Button btnDelete = new Button();
        private readonly Label lblStatus = new Label();
        private IList<ManualServer> servers = new List<ManualServer>();
        private Guid? selectedId;

        public frmManualServers()
        {
            Text = "Manage Manual Servers";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(610, 390);
            BackColor = Color.FromArgb(32, 32, 32);
            ForeColor = Color.Gainsboro;

            lstServers.SetBounds(12, 12, 245, 320);
            lstServers.BackColor = Color.FromArgb(22, 22, 22);
            lstServers.ForeColor = Color.Gainsboro;
            lstServers.SelectedIndexChanged += LstServers_SelectedIndexChanged;
            Controls.Add(lstServers);

            AddLabel("Server name", 275, 15);
            txtName.SetBounds(275, 36, 320, 25);
            AddControl(txtName);

            AddLabel("Hostname / IP", 275, 70);
            txtHost.SetBounds(275, 91, 320, 25);
            AddControl(txtHost);

            AddLabel("Port", 275, 125);
            numPort.SetBounds(275, 146, 110, 25);
            numPort.Minimum = 1;
            numPort.Maximum = 65535;
            numPort.Value = 443;
            AddControl(numPort);

            AddLabel("Username", 275, 180);
            txtUsername.SetBounds(275, 201, 320, 25);
            AddControl(txtUsername);

            AddLabel("Password", 275, 235);
            txtPassword.SetBounds(275, 256, 320, 25);
            txtPassword.UseSystemPasswordChar = true;
            AddControl(txtPassword);

            btnNew.Text = "New";
            btnNew.SetBounds(12, 342, 75, 30);
            btnNew.Click += (s, e) => ClearEditor();
            AddControl(btnNew);

            btnDelete.Text = "Delete";
            btnDelete.SetBounds(182, 342, 75, 30);
            btnDelete.Click += Delete_Click;
            AddControl(btnDelete);

            btnSave.Text = "Save";
            btnSave.SetBounds(520, 300, 75, 32);
            btnSave.Click += Save_Click;
            AddControl(btnSave);

            lblStatus.SetBounds(275, 342, 320, 30);
            lblStatus.ForeColor = Color.Silver;
            AddControl(lblStatus);

            LoadServers();
        }

        private void AddLabel(string text, int x, int y)
        {
            var label = new Label { Text = text, AutoSize = true, ForeColor = Color.Gainsboro };
            label.SetBounds(x, y, 300, 20);
            Controls.Add(label);
        }

        private void AddControl(Control control)
        {
            control.Font = new Font("Segoe UI", 9F);
            Controls.Add(control);
        }

        private void LoadServers()
        {
            try
            {
                servers = ManualServerStore.Load();
                RefreshList();
                lblStatus.Text = servers.Count + " saved server(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load manual servers.\n\n" + ex.Message,
                    "Manual servers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                servers = new List<ManualServer>();
            }
        }

        private void RefreshList()
        {
            lstServers.BeginUpdate();
            lstServers.Items.Clear();
            foreach (var server in servers)
                lstServers.Items.Add(new ServerListItem(server));
            lstServers.EndUpdate();
        }

        private void LstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lstServers.SelectedItem as ServerListItem;
            if (item == null)
                return;

            selectedId = item.Server.Id;
            txtName.Text = item.Server.Name;
            txtHost.Text = item.Server.Hostname;
            numPort.Value = item.Server.Port;
            txtUsername.Text = item.Server.Username;

            try
            {
                txtPassword.Text = ManualServerStore.GetPassword(item.Server.Id);
                lblStatus.Text = "Selected: " + item.Server.Hostname;
            }
            catch (Exception ex)
            {
                txtPassword.Clear();
                lblStatus.Text = "Password unavailable: " + ex.Message;
            }
        }

        private void ClearEditor()
        {
            lstServers.ClearSelected();
            selectedId = null;
            txtName.Clear();
            txtHost.Clear();
            numPort.Value = 443;
            txtUsername.Clear();
            txtPassword.Clear();
            lblStatus.Text = "Enter the new server details.";
            txtName.Focus();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var server = new ManualServer
            {
                Id = selectedId ?? Guid.NewGuid(),
                Name = txtName.Text.Trim(),
                Hostname = txtHost.Text.Trim(),
                Port = Decimal.ToInt32(numPort.Value),
                Username = txtUsername.Text.Trim()
            };

            if (!server.IsValid())
            {
                MessageBox.Show("Hostname and username are required, and the port must be valid.",
                    "Invalid server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ManualServerStore.Save(server, txtPassword.Text);
                servers = ManualServerStore.Load();
                RefreshList();
                for (int i = 0; i < lstServers.Items.Count; i++)
                {
                    var item = (ServerListItem)lstServers.Items[i];
                    if (item.Server.Id == server.Id)
                    {
                        lstServers.SelectedIndex = i;
                        break;
                    }
                }
                lblStatus.Text = "Server saved. Password protected with Windows DPAPI.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the server.\n\n" + ex.Message,
                    "Manual servers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue)
                return;

            if (MessageBox.Show("Delete the selected saved server?", "Confirm delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                ManualServerStore.Delete(selectedId.Value);
                servers = ManualServerStore.Load();
                RefreshList();
                ClearEditor();
                lblStatus.Text = "Server deleted.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to delete the server.\n\n" + ex.Message,
                    "Manual servers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private sealed class ServerListItem
        {
            public ManualServer Server { get; private set; }
            public ServerListItem(ManualServer server) { Server = server; }
            public override string ToString()
            {
                return string.IsNullOrWhiteSpace(Server.Name) ? Server.Hostname : Server.Name + " (" + Server.Hostname + ")";
            }
        }
    }
}
