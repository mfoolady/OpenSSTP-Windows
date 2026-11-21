using System;

namespace OpenSSTP.BAL
{
    /// <summary>
    /// A user-managed VPN server. Credentials are deliberately not stored on this model.
    /// </summary>
    internal sealed class ManualServer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }

        public ManualServer()
        {
            Id = Guid.NewGuid();
            Port = 443;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Hostname)
                && Port == 443
                && !string.IsNullOrWhiteSpace(Username);
        }
    }
}
