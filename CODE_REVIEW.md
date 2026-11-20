# Initial Code Review

**Review scope:** Static inspection of the repository at the `improve/project-foundation` branch, based on the current `.NET Framework 4.8` source.

**Review type:** Initial static review only. The application has not been built or executed as part of this review. Findings below should be verified in a Windows build/test environment before behavior-changing fixes are merged.

## Findings

### High — Disconnect performs setup work when there is no dialer handle

**Location:** `frmMain.cs`, `frmMain_Load()` and `Disconnect()`

When startup detects that the application is not connected, `frmMain_Load()` calls `Disconnect()`. In the branch where `handle` is null, `Disconnect()` calls `CreateConnection()`, which opens a phonebook, creates a VPN entry, and then removes that entry. This is unnecessary work on the normal disconnected startup path and can fail if the expected SSTP device or phonebook is unavailable.

**Recommended direction:** Make disconnect a state-only operation: cancel an in-progress dial attempt if needed, find and hang up only the application's actual active connection, and avoid creating a VPN entry as part of disconnect. Define and test the intended profile-retention behavior before changing phonebook deletion.

### High — Error handler assumes an active RAS connection exists

**Location:** `frmMain.cs`, `Dialer_Error()`

The handler indexes `RasConnection.GetActiveConnections()[0]` without checking whether the returned collection contains an item. An error raised before a connection is established can therefore cause a second exception while handling the original error.

**Recommended direction:** Check for an active connection matching `ConnectionName`; handle an empty collection safely and report the original dialer exception regardless of connection state.

### High — Server-list JSON and fallback data are not validated

**Locations:** `frmListCustom.cs`, `GetServerListAsync()`; `frmServerList.cs`, `GetServerListAsync()`; `BAL/Server.cs`

The remote response is deserialized and immediately used with LINQ. A null result, malformed JSON, or unexpected data can throw. The catch path then reads and deserializes the local cache and immediately calls `ToList()`, which can fail in the same way. The broad catch also hides whether the network request, parsing, or file write failed.

**Recommended direction:** Validate that deserialization produced a non-null list, validate required server fields before displaying or saving entries, isolate remote-fetch and cache-read errors, and show a useful offline/error state when neither source is usable.

### Medium — HTTP requests have no explicit timeout or cancellation policy

**Locations:** `frmMain.cs`, `CheckUpdateAsync()`; `frmListCustom.cs` and `frmServerList.cs`, `GetServerListAsync()`

The code uses `HttpClient.GetStringAsync()` without an application-specific timeout or cancellation path. A slow or unavailable endpoint can leave the UI waiting for a long time.

**Recommended direction:** Define a bounded request timeout and cancellation behavior appropriate to the UI lifecycle. Keep network operations asynchronous and surface non-sensitive diagnostics.

### Medium — Dialer event handlers are added on every connect attempt

**Location:** `frmMain.cs`, `Connect()`

The `Error`, `StateChanged`, and `DialCompleted` handlers are attached each time `Connect()` starts a dial. They are not detached in the shown lifecycle. Repeated attempts may cause duplicate callbacks and retain references longer than intended.

**Recommended direction:** Subscribe once for the dialer's lifetime, or pair every subscription with deterministic unsubscription. Dispose the dialer and phonebook resources according to a clearly defined lifecycle.

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

1. Add safe RAS connection enumeration and error handling; define profile lifecycle behavior.
2. Harden remote and cached JSON loading with explicit validation and useful error states.
3. Define HTTP timeout/cancellation and event/resource lifetimes.
4. Reconcile version metadata and signing/build configuration.
5. Consolidate duplicate UI/data-loading paths after behavior is covered by tests.

## Verification still required

- Clean Debug and Release builds on Windows with the .NET Framework 4.8 Developer Pack.
- Verify behavior with no active VPN connection, an existing application connection, an unrelated active RAS connection, and a failed/cancelled dial attempt.
- Verify server-list loading with valid, empty, malformed, and unavailable remote/cache data.
- Verify the intended ClickOnce or other publishing/signing workflow.
- Add automated tests where feasible; Windows RAS integration also requires manual testing on supported Windows versions.
