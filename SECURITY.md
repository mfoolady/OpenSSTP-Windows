# Security Policy

## Reporting a vulnerability

Please do not report exploitable vulnerabilities, credentials, private keys, or sensitive connection data in a public issue.

Use GitHub's private vulnerability reporting feature for this repository if it is enabled. If private reporting is unavailable, contact the repository maintainer privately through a verified contact method associated with the repository profile.

Include, where possible:

- A concise description of the issue and its impact
- Affected version or commit
- Reproduction steps or a proof of concept that does not expose real user data
- Any suggested mitigation

Please allow reasonable time for investigation and a fix before public disclosure.

## Sensitive data

The application may store configuration and connection records in local JSON files. Treat these files as potentially sensitive. Never attach unredacted user data, credentials, tokens, private keys, or connection details to public reports.

## Scope

Security reviews should pay particular attention to credential handling, local file permissions, downloaded server-list/update data, JSON parsing, network communication, and the VPN connection lifecycle.

This policy does not guarantee a response time or imply that a formal security response program is staffed.
