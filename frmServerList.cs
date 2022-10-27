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
    public partial class frmServerList : Form
    {
        internal event SelectServerHandler OnSelectServer;
        
        private int _previousIndex;
        private bool _sortDirection;
        List<Server> list = new List<Server>();

        public frmServerList()
        {
            InitializeComponent();
        }

        private void frmServerList_Load(object sender, EventArgs e)
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
                    dataGridView.DataSource = list;
                }
            }
            catch
            {
                if (File.Exists(@"Records.json"))
                {
                    string json = File.ReadAllText(@"Records.json");
                    list = JsonConvert.DeserializeObject<List<Server>>(json).ToList();
                    dataGridView.DataSource = list;
                }
            }
            finally
            {
                prgLoading.Visible = false;
            }
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (list.Count > 0)
            {
                if (e.ColumnIndex == _previousIndex)
                    _sortDirection ^= true; // toggle direction

                dataGridView.DataSource = SortData((List<Server>)dataGridView.DataSource, dataGridView.Columns[e.ColumnIndex].Name, _sortDirection);

                _previousIndex = e.ColumnIndex;
            }
        }

        private List<Server> SortData(List<Server> mlist, string column, bool ascending)
        {
            if (column == "UPTIME")
                return ascending ?
                mlist.OrderBy(_ => ToMinute(_.GetType().GetProperty(column).GetValue(_).ToString())).ToList() :
                mlist.OrderByDescending(_ => ToMinute(_.GetType().GetProperty(column).GetValue(_).ToString())).ToList();
            else if (column == "PING" || column == "LINE_QUALITY")
                return ascending ?
                mlist.OrderBy(_ => ToDigit(_.GetType().GetProperty(column).GetValue(_).ToString())).ToList() :
                mlist.OrderByDescending(_ => ToDigit(_.GetType().GetProperty(column).GetValue(_).ToString())).ToList();
            else
            return ascending ?
                mlist.OrderBy(_ => _.GetType().GetProperty(column).GetValue(_)).ToList() :
                mlist.OrderByDescending(_ => _.GetType().GetProperty(column).GetValue(_)).ToList();
        }

        private long ToMinute(string dirty)
        {
            int num = (int)ToDigit(dirty);

            if (dirty.Contains("month"))
                return num * 60 * 24 * 30;
            else if (dirty.Contains("week"))
                return num * 60 * 24 * 7;
            else if (dirty.Contains("day"))
                return num * 60 * 24;
            else if (dirty.Contains("hour"))
                return num * 60;
            else
                return num;
        }

        private float ToDigit(string dirty)
        {
           return float.Parse(Regex.Replace(dirty, "[^0-9.]", ""));
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var row = dataGridView.Rows[e.RowIndex];

                Server server = new Server()
                {
                    HOSTNAME = row.Cells["HOSTNAME"].Value.ToString(),
                    LOCATION = row.Cells["LOCATION"].Value.ToString(),
                    FLAG = row.Cells["FLAG"].Value.ToString(),
                    PING = row.Cells["PING"].Value.ToString(),
                    PORT = int.Parse(row.Cells["PORT"].Value.ToString()),
                    UPTIME = row.Cells["UPTIME"].Value.ToString(),
                    SESSIONS = int.Parse(row.Cells["SESSIONS"].Value.ToString()),
                    LINE_QUALITY = row.Cells["LINE_QUALITY"].Value.ToString(),
                    SCORE = int.Parse(row.Cells["SCORE"].Value.ToString())
                };

                if (MessageBox.Show($"Change the VPN connection to \n{server.LOCATION} ::: \"{server.HOSTNAME}\"?", "Change the VPN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                    OnSelectServer?.Invoke(server);
                    this.Close();
                }
            }

        }
    }
}
