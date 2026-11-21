using System;

namespace OpenSSTP.BAL
{
    internal enum VpnServerType
    {
        Free,
        Manual
    }

    /// <summary>
    /// A credential-free description of the endpoint selected for a VPN connection.
    /// Secrets are resolved separately by the connection workflow.
    /// </summary>
    internal sealed class VpnServerProfile
    {
        public VpnServerType Type { get; private set; }
        public string Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Location { get; private set; }
        public string Flag { get; private set; }
        public Server FreeServer { get; private set; }
        public ManualServer ManualServer { get; private set; }

        private VpnServerProfile() { }

        public static VpnServerProfile FromFreeServer(Server server)
        {
            if (server == null)
                throw new ArgumentNullException("server");

            return new VpnServerProfile
            {
                Type = VpnServerType.Free,
                Id = server.HOSTNAME,
                DisplayName = string.IsNullOrWhiteSpace(server.LOCATION) ? server.HOSTNAME : server.LOCATION,
                Hostname = server.HOSTNAME,
                Port = server.PORT,
                Location = server.LOCATION,
                Flag = server.FLAG,
                FreeServer = server
            };
        }

        public static VpnServerProfile FromManualServer(ManualServer server)
        {
            if (server == null)
                throw new ArgumentNullException("server");
            if (!server.IsValid())
                throw new ArgumentException("The manual server is invalid.", "server");

            return new VpnServerProfile
            {
                Type = VpnServerType.Manual,
                Id = server.Id.ToString("D"),
                DisplayName = string.IsNullOrWhiteSpace(server.Name) ? server.Hostname : server.Name,
                Hostname = server.Hostname,
                Port = server.Port,
                Username = server.Username,
                ManualServer = server
            };
        }
    }
}
