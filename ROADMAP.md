# Roadmap

This roadmap is a working plan, not a promise that every item is already implemented.

## Phase 1 — Project foundation

- [x] Add project overview, contribution guidance, security policy, and license.
- [ ] Verify the current solution builds from a clean Windows environment.
- [ ] Document the exact build prerequisites and release process.
- [ ] Identify supported Windows versions and establish a repeatable smoke-test checklist.

## Phase 2 — Reliability

- [ ] Review VPN connect/disconnect lifecycle and RAS connection enumeration.
- [ ] Make error paths safe when no active RAS connection exists.
- [ ] Review event subscription/unsubscription and disposal of network resources.
- [ ] Improve HTTP timeout, cancellation, and failure handling.
- [ ] Validate downloaded JSON and local configuration before use.
- [ ] Replace silent exception handling with useful, privacy-safe diagnostics.

## Phase 3 — Security and maintainability

- [ ] Review handling and storage of credentials and connection records.
- [ ] Ensure secrets and signing keys are never committed.
- [ ] Centralize application version and build metadata.
- [ ] Add automated tests for logic that can be tested independently of Windows RAS.
- [ ] Review dependencies and update them deliberately with compatibility checks.

## Phase 4 — Gradual modernization

- [ ] Separate UI concerns from connection and data-access logic where practical.
- [ ] Introduce interfaces or abstractions only where they improve testability.
- [ ] Evaluate a newer .NET target and migration path.
- [ ] Preserve existing behavior through documented compatibility tests.

## Phase 5 — User experience and releases

- [ ] Improve user-facing connection and update error messages.
- [ ] Document installation, upgrade, and recovery procedures.
- [ ] Establish versioning, release notes, and reproducible release builds.

## Principles

- Keep `main` stable; develop through branches and pull requests.
- Prefer small changes with clear evidence and test results.
- Do not claim compatibility or test coverage without verification.
