using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OpenSSTP.BAL
{
    /// <summary>
    /// Stores manual-server metadata and DPAPI-protected passwords for the current
    /// Windows user. The file is not portable between Windows user accounts.
    /// </summary>
    internal static class ManualServerStore
    {
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("OpenSSTP.ManualServerStore.v1");

        private sealed class StoredServer
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Hostname { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string ProtectedPassword { get; set; }
        }

        private static string StorePath
        {
            get
            {
                string directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "OpenSSTP");
                return Path.Combine(directory, "manual-servers.json");
            }
        }

        public static IList<ManualServer> Load()
        {
            if (!File.Exists(StorePath))
                return new List<ManualServer>();

            string json = File.ReadAllText(StorePath, Encoding.UTF8);
            var records = JsonConvert.DeserializeObject<List<StoredServer>>(json);
            if (records == null)
                throw new InvalidDataException("The manual server store has an invalid format.");

            var result = new List<ManualServer>();
            foreach (var record in records)
            {
                if (record == null || record.Id == Guid.Empty ||
                    string.IsNullOrWhiteSpace(record.Hostname) ||
                    record.Port < 1 || record.Port > 65535 ||
                    string.IsNullOrWhiteSpace(record.Username))
                    continue;

                result.Add(new ManualServer
                {
                    Id = record.Id,
                    Name = record.Name,
                    Hostname = record.Hostname,
                    Port = record.Port,
                    Username = record.Username
                });
            }

            return result;
        }

        public static string GetPassword(Guid serverId)
        {
            if (serverId == Guid.Empty)
                throw new ArgumentException("A valid server ID is required.", "serverId");

            var records = ReadRecords();
            var record = records.FirstOrDefault(x => x.Id == serverId);
            if (record == null || string.IsNullOrEmpty(record.ProtectedPassword))
                return string.Empty;

            byte[] encrypted = Convert.FromBase64String(record.ProtectedPassword);
            byte[] clear = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(clear);
        }

        public static void Save(ManualServer server, string password)
        {
            if (server == null)
                throw new ArgumentNullException("server");
            if (server.Id == Guid.Empty)
                server.Id = Guid.NewGuid();
            if (!server.IsValid())
                throw new ArgumentException("The manual server details are incomplete or invalid.", "server");

            var records = ReadRecords();
            byte[] clear = Encoding.UTF8.GetBytes(password ?? string.Empty);
            byte[] encrypted = ProtectedData.Protect(clear, Entropy, DataProtectionScope.CurrentUser);

            var record = records.FirstOrDefault(x => x.Id == server.Id);
            if (record == null)
            {
                record = new StoredServer { Id = server.Id };
                records.Add(record);
            }

            record.Name = server.Name;
            record.Hostname = server.Hostname.Trim();
            record.Port = server.Port;
            record.Username = server.Username.Trim();
            record.ProtectedPassword = Convert.ToBase64String(encrypted);

            WriteRecords(records);
        }

        public static void Delete(Guid serverId)
        {
            if (serverId == Guid.Empty)
                return;

            var records = ReadRecords();
            records.RemoveAll(x => x.Id == serverId);
            WriteRecords(records);
        }

        private static List<StoredServer> ReadRecords()
        {
            if (!File.Exists(StorePath))
                return new List<StoredServer>();

            string json = File.ReadAllText(StorePath, Encoding.UTF8);
            var records = JsonConvert.DeserializeObject<List<StoredServer>>(json);
            if (records == null)
                throw new InvalidDataException("The manual server store has an invalid format.");
            return records;
        }

        private static void WriteRecords(List<StoredServer> records)
        {
            string path = StorePath;
            string directory = Path.GetDirectoryName(path);
            Directory.CreateDirectory(directory);

            string temporaryPath = path + ".tmp";
            File.WriteAllText(temporaryPath, JsonConvert.SerializeObject(records, Formatting.Indented), Encoding.UTF8);

            if (File.Exists(path))
                File.Replace(temporaryPath, path, null);
            else
                File.Move(temporaryPath, path);
        }
    }
}
