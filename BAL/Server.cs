using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSTP.BAL
{
    internal class Server
    {
        public string HOSTNAME { get; set; }
        public string LOCATION { get; set; }
        public string FLAG { get; set; }
        public string PING { get; set; }
        public int PORT { get; set; }
        public string UPTIME { get; set; }
        public int SESSIONS { get; set; }
        public string LINE_QUALITY { get; set; }
        public int SCORE { get; set; }


        public void SaveToFile()
        {
            File.WriteAllText(@"config.json", JsonConvert.SerializeObject(this));
        }

        public static Server LoadConfigFromFile()
        {
            if (File.Exists(@"config.json"))
            {
                string json = File.ReadAllText(@"config.json");
                return JsonConvert.DeserializeObject <Server>(json);
            }
            return null;
        }

    }


}
