# OpenSSTP-Windows

OpenSSTP-Windows is a Windows desktop application for managing SSTP VPN connections through the Windows Remote Access Service (RAS).

## Project status

This repository is undergoing gradual maintenance and modernization. The initial priority is to preserve existing behavior while improving reliability, diagnostics, security, and maintainability. Larger architectural changes will be considered incrementally and documented before implementation.

## Technology

The current application is based on:

- C# and Windows Forms
- .NET Framework 4.8
- Windows RAS, with DotRas
- Newtonsoft.Json

The application retrieves server-list and update metadata from an external service. Review the relevant source and service behavior before relying on that service or changing its integration.

## Build

1. Use Windows with Visual Studio and the .NET Framework 4.8 Developer Pack.
2. Clone this repository and open the solution/project in Visual Studio.
3. Restore the NuGet dependencies.
4. Build the solution in the required configuration.

Some environments may report a signing/build error because the project references `OpenSSTP_TemporaryKey.pfx`, which is not present in the repository. Do not commit a private signing key. If signing is required, configure a locally managed certificate or update the signing configuration as part of a reviewed change.

A successful build has not yet been verified across supported Windows versions. Please report the Windows version, build configuration, and complete error output when filing build issues.

## Local data

The application uses local JSON data files, including `config.json` and `Records.json`. Treat these files as potentially sensitive user data. Do not publish real credentials, connection records, or personal information in issues or pull requests.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for the proposed workflow and coding expectations. Please use focused branches and pull requests; do not push directly to `main`.

## Security

For security-sensitive reports, follow [SECURITY.md](SECURITY.md). Do not disclose credentials, private keys, or exploitable details in public issues.

## License

This project is distributed under the MIT License. See [LICENSE](LICENSE).
