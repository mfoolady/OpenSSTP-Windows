# Initial Code Review

**Review scope:** Static inspection of the repository at the `improve/project-foundation` branch, based on the current `.NET Framework 4.8` source.

**Review type:** Initial static review only. The application has not been built or executed as part of this review. Findings below should be verified in a Windows build/test environment before behavior-changing fixes are merged.

## Follow-up implementation status

The following source-level fixes have since been committed to this branch. They are **not yet runtime-verified** because no Windows/.NET Framework build or RAS integration test has been run.

| Finding | Source change | Verification status |
|---|---|---|
| Disconnect creates a profile while disconnected | Disconnect no longer calls connection setup; it searches for the active app connection, and attempts to remove an existing app profile without creating one. | Static source review only; test on Windows. |
| RAS error handler assumes an active connection | It now searches for the app's connection safely and displays the dialer exception when available. | Static source review only; test failed dial attempts. |
| Remote/cache server-list JSON is not validated | Remote and cached lists are validated for usable SSTP entries; cache fallback and a user-visible unavailable state were added. | Static source review only; test malformed/empty/offline cases. |
| Local selected-server JSON is not validated | Invalid or inaccessible config files now return no selected server, allowing the user to choose one. | Static source review only; test malformed and inaccessible config files. |
| RAS event handlers are attached repeatedly | Event handlers are subscribed once in the form constructor rather than on each connect attempt. | Static source review only; test repeated connect/disconnect cycles. |


### Manual-server selection and connection routing (implemented; not runtime-verified)

The branch now includes a credential-free `VpnServerProfile` abstraction shared by free and manual endpoints. The manual-server manager can return a selected saved server; the server-list dialog confirms that selection and sends it through the same selection event used by free servers. `frmMain` builds the RAS entry from the selected profile and resolves manual credentials from `ManualServerStore` only when dialing. Free servers retain the existing `vpn/vpn` credentials and local selected-server configuration behavior.

Static review notes:
- Password encryption uses `DataProtectionScope.CurrentUser`; migration to another Windows account or machine is not supported by this storage format.
- Passwords are not properties of `VpnServerProfile`; the dial path retrieves the protected password by the manual server ID immediately before creating the `NetworkCredential`.
- The selected manual endpoint is held in memory for the current application session; unlike free-server selection, it is not yet restored automatically after an application restart.
- Manual entries are currently restricted to port 443. Windows RAS documents the SSTP endpoint as a DNS name or IP address and does not expose a portable custom-port parameter in the DotRas entry factory; custom-port support is intentionally deferred rather than relying on an unverified `hostname:port` convention.
- A failed connection setup attempts to remove a partially-created application RAS profile.
- DPAPI decryption failures and malformed store data are surfaced; recovery/backup behavior should be considered before release.
- These changes have not been compiled or exercised on Windows. Do not treat manual VPN support as production-verified until the build and real SSTP connection tests pass.

## Findings

### High — Disconnect performs setup work when there is no dialer handle

**Location:** `frmMain.cs`, `frmMain_Load()` and `Disconnect()`

**Status: addressed in this branch (static review only).** `Disconnect()` no longer calls `CreateConnection()`; it cancels a pending dial, looks up only the application's named RAS connection, and removes an existing application profile when the phonebook is available. Windows testing is still required for disconnected startup, active connections, and phonebook permissions.

### High — Error handler assumes an active RAS connection exists

**Location:** `frmMain.cs`, `Dialer_Error()`

**Status: addressed in this branch (static review only).** The handler now searches for a connection matching `ConnectionName`, handles the no-connection case, and reports the original dialer exception. Verify failed and cancelled dial attempts on Windows.

### High — Server-list JSON and fallback data are not validated

**Locations:** `frmListCustom.cs`, `GetServerListAsync()`; `frmServerList.cs`, `GetServerListAsync()`; `BAL/Server.cs`

**Status: partially addressed.** `frmListCustom` and `BAL.Server.LoadConfigFromFile()` validate deserialized data and provide cache fallback/error handling. The legacy `frmServerList` implementation remains and should be removed or brought to the same validation standard after confirming whether it is still used.

### Medium — HTTP requests have no explicit timeout or cancellation policy

**Locations:** `frmMain.cs`, `CheckUpdateAsync()`; `frmListCustom.cs` and `frmServerList.cs`, `GetServerListAsync()`

**Status: partially addressed.** Explicit 10-second and 15-second timeouts exist in the update check and active server-list form. Cancellation tied to form lifetime is not implemented, and the legacy `frmServerList` path should be reviewed if retained.

### Medium — Dialer event handlers are added on every connect attempt

**Location:** `frmMain.cs`, `Connect()`

**Status: addressed in this branch (static review only).** The three handlers are subscribed once in the form constructor. Repeated connect/disconnect cycles still need Windows runtime testing; full dialer disposal/lifetime cleanup can be handled separately.

### Medium — Application version values are inconsistent

**Locations:** `frmMain.cs`, `OpenSSTP.csproj`, `Properties/AssemblyInfo.cs`

The UI/update check uses `1.1.0` / `110`, the assembly version is `1.1.0.0`, while the project ClickOnce `ApplicationVersion` remains `1.0.1.0`. These values can drift and cause confusing version or update behavior.

**Recommended direction:** Establish one documented versioning source and align assembly and publish metadata. Do not change update semantics until the external version feed format and release process are confirmed.

### Medium — Manifest signing references a missing private key file

**Location:** `OpenSSTP.csproj`

The project sets `SignManifests` to `true` and references `OpenSSTP_TemporaryKey.pfx`, but that file is not present in the repository tree. A clean build or publish may fail when the local signing key is unavailable.

**Recommended direction:** Verify clean Debug/Release builds and publishing on Windows. Keep private signing keys out of source control; configure local signing or a controlled release-signing process. Do not simply disable signing without checking the intended ClickOnce/publishing workflow.

### Low — Duplicate server-list implementation

**Locations:** `frmServerList.cs` and `frmListCustom.cs`

Both forms implement remote server-list download, local caching, and JSON deserialization. The main form currently opens `frmListCustom`, while the other form appears to be an alternate/legacy implementation.

**Recommended direction:** Confirm whether `frmServerList` is intentionally retained. If it is unused, plan removal or consolidation separately after confirming there are no external references.

## Suggested implementation order

1. Verify the RAS lifecycle fixes and unified free/manual credential routing on Windows (implemented on this branch; runtime validation pending).
2. Finish hardening the legacy server-list implementation or remove it after confirming it is unused.
3. Add HTTP cancellation and complete dialer/resource lifetime management.
4. Reconcile version metadata and verify the ClickOnce signing/publishing workflow.
5. Consolidate duplicate UI/data-loading paths after behavior is covered by tests.

## Verification still required

- Clean Debug and Release builds on Windows with the .NET Framework 4.8 Developer Pack.
- Verify behavior with no active VPN connection, an existing application connection, an unrelated active RAS connection, and a failed/cancelled dial attempt.
- Verify selecting, connecting, disconnecting, and reconnecting to a saved manual server; test wrong credentials, deleted server records, DPAPI failure, enforcement of port 443, hostnames, IPv4, and IPv6.
- Verify server-list loading with valid, empty, malformed, and unavailable remote/cache data.
- Verify the intended ClickOnce or other publishing/signing workflow.
- Add automated tests where feasible; Windows RAS integration also requires manual testing on supported Windows versions.
