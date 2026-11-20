# Contributing

Thank you for helping maintain OpenSSTP-Windows.

## Development approach

The project follows gradual modernization:

1. Preserve existing user-visible behavior unless a change is intentional and documented.
2. Fix correctness and reliability issues in small, reviewable changes.
3. Add or improve tests around behavior before making broad refactors where practical.
4. Keep changes compatible with the current .NET Framework 4.8 baseline unless a migration is explicitly approved.
5. Document behavior changes, compatibility implications, and manual test steps.

## Workflow

1. Create a focused branch from `main`.
2. Keep each pull request limited to one concern.
3. Explain the problem, the chosen solution, and any trade-offs.
4. Include reproduction steps for bug fixes and tests or manual verification performed.
5. Do not merge unreviewed changes directly into `main`.

## Code expectations

- Follow the existing code style unless the change is specifically improving consistency.
- Prefer clear, defensive handling of external input, network failures, and local file operations.
- Avoid swallowing exceptions silently; provide actionable diagnostics without exposing secrets.
- Dispose disposable resources and unsubscribe event handlers when their lifetime ends.
- Do not add credentials, private keys, personal data, or machine-specific configuration to the repository.
- Avoid unrelated formatting or large-scale refactoring in bug-fix pull requests.

## Testing

For changes affecting VPN connection behavior, test both successful and failed connection paths where possible, including the absence of active RAS connections. For network and JSON handling, test malformed responses, unavailable services, and invalid local data.

Clearly distinguish automated tests from manual checks. Do not claim a platform or scenario was tested unless it actually was.
