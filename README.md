# OpenSSTP-Windows

OpenSSTP-Windows is a Windows desktop application for managing SSTP VPN connections through the Windows Remote Access Service (RAS).

## Download

**Latest release: [OpenSSTP 1.2.0](https://github.com/mfoolady/OpenSSTP-Windows/releases/tag/v1.2.0)**

- [Download OpenSSTP 1.2.0 (ZIP)](https://github.com/mfoolady/OpenSSTP-Windows/releases/download/v1.2.0/OpenSSTP.1.2.0.zip)
- [View all releases](https://github.com/mfoolady/OpenSSTP-Windows/releases)

Download the ZIP file, extract it to a folder, and run the application executable. This release is distributed as a ZIP package; it is not a conventional setup wizard.

### Requirements

- Windows 10 or Windows 11.
- .NET Framework 4.8.
- An available Windows SSTP VPN/RAS component.
- Internet access to retrieve the public server list (unless a usable cached list is available).

If the application does not start because the .NET Framework is missing, install the .NET Framework 4.8 runtime from Microsoft.

## How to use

### Connect to a free server

1. Start OpenSSTP-Windows.
2. Click the server-selection control to open the server list.
3. Wait for the list to load. The application retrieves the list from the [FreeSSTP server-list repository](https://github.com/FreeSSTP/server-list). If the remote list cannot be reached, it attempts to use the local `Records.json` cache.
4. Select a server and confirm the selection.
5. Return to the main window and click the connection control to connect.
6. To disconnect, click the connection control again or close the application and confirm disconnection if prompted.

Free-server availability, speed, and reliability depend on the third-party server operators. OpenSSTP does not operate or guarantee these servers.

### Add and use a manual server

1. Open the server-selection window.
2. Click **Manage Manual Servers**.
3. Click **New** and enter a server name (optional), hostname or IP address, username, and password.
4. Click **Save**. The password is protected using Windows DPAPI and is stored for the current Windows user.
5. Select the saved server and click **Use selected**.
6. Confirm the selection, return to the main window, and click the connection control.

The current implementation uses SSTP on port **443**. Custom ports are not currently supported by the manual-server interface.

Manual server details are stored locally and remain available after disconnecting. The temporary Windows RAS connection profile created by the application is removed during disconnect/cleanup. Because password protection uses the current Windows user account, saved passwords are not portable to another Windows account or computer.

### Troubleshooting

- **The server list is empty:** Check your internet connection. The application can fall back to a local `Records.json` cache only if a valid cache exists.
- **A connection fails:** The selected server may be offline or may reject the supplied credentials. Try another server or verify the manual server hostname, username, and password.
- **A saved manual password cannot be read:** Make sure you are signed in to the same Windows user account that saved it. DPAPI-protected passwords cannot normally be decrypted under a different account.
- **The connection profile remains in Windows:** Disconnect from the application and check its displayed status. If cleanup reports an error, include that message when reporting the issue.

## Project status

The project is undergoing gradual maintenance and modernization. The current priority is to preserve existing behavior while improving reliability, diagnostics, security, and maintainability. Larger architectural changes will be considered incrementally and documented before implementation.

Version 1.2.0 includes manual SSTP server management, protected local password storage, and connection-lifecycle fixes.

### Build status and testing

The Windows GitHub Actions build for version 1.2.0 completed successfully. The build produced the release ZIP with **0 errors and 6 compiler warnings**.

A successful CI build does not mean that every runtime scenario has been tested. Real-world SSTP connection tests, credential validation, and profile-cleanup behavior still need verification on supported Windows systems.

The CI package is built with ClickOnce manifest signing disabled because the repository does not contain the private signing certificate. Do not commit a private signing key to the repository.

## Technology

The current application is based on:

- C# and Windows Forms
- .NET Framework 4.8
- Windows RAS, with DotRas
- Newtonsoft.Json

The application retrieves the public server list and update metadata from external services. Review those services and their behavior before relying on them or changing their integration.

## Build from source

1. Use Windows with Visual Studio and the .NET Framework 4.8 Developer Pack.
2. Clone this repository and open `OpenSSTP.sln` in Visual Studio.
3. Restore the NuGet dependencies.
4. Build the solution in the required configuration.

Some local build configurations may report a signing error because the project references `OpenSSTP_TemporaryKey.pfx`, which is not present in the repository. Do not commit a private signing key. If signing is required, configure a locally managed certificate or a secure CI signing secret as part of a reviewed change.

## Local data and privacy

The application uses local JSON data files, including `config.json` and `Records.json`. Manual server definitions and DPAPI-protected passwords are stored under the current user's local application data directory.

Treat these files as potentially sensitive user data. Do not publish real credentials, connection records, or personal information in issues or pull requests. DPAPI-protected passwords are tied to the Windows user account that saved them.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for the proposed workflow and coding expectations. Please use focused branches and pull requests; do not push directly to `main`.

## Security

For security-sensitive reports, follow [SECURITY.md](SECURITY.md). Do not disclose credentials, private keys, or exploitable details in public issues.

## License

This project is distributed under the MIT License. See [LICENSE](LICENSE).
