<#
.SYNOPSIS
  Copies the shared UI core from Athena Web 2 into Athena Remastered (one-way).

.DESCRIPTION
  Athena Web 2 owns the shared UI core (`ui/src/core/`). Athena Remastered
  (AR) consumes it as a copy in `Frontend/src/core/`. Sync is strictly
  one-way, Web2 -> AR; see docs/ui-core-sync.md.

  What the script does:
    1. Refuses to run when `ui/src/core/` has uncommitted changes in Web2
       (the manifest must point at a real commit), unless -AllowDirty.
    2. Drift check: if AR already has a manifest, every core file in AR is
       hashed and compared with it. Files edited by hand in AR (which would
       break the one-way rule) abort the sync, unless -Force.
    3. Mirrors `ui/src/core/` into AR: copies new/changed files and deletes
       files that no longer exist in Web2.
    4. Writes `Frontend/src/core/core-manifest.json` with the Web2 commit,
       timestamp and SHA-256 of every file.

  The script only changes files inside AR's `Frontend/src/core/`. It never
  commits; review and commit in the AR repo on a branch (CLAUDE.md workflow).

.PARAMETER ArRepo
  Path of the Athena Remastered repository. Default: C:\AthenaRemastered.

.PARAMETER WhatIf
  Show what would change without writing anything.

.PARAMETER Force
  Overwrite AR core files even if they were edited in AR (drift).

.PARAMETER AllowDirty
  Allow syncing uncommitted Web2 core changes (the manifest then records
  "<commit>-dirty").

.EXAMPLE
  .\scripts\sync-core-to-ar.ps1 -WhatIf
  .\scripts\sync-core-to-ar.ps1
#>
param(
  [string]$ArRepo = 'C:\AthenaRemastered',
  [switch]$WhatIf,
  [switch]$Force,
  [switch]$AllowDirty
)

$ErrorActionPreference = 'Stop'

# Name of the manifest file written into AR's core folder.
$ManifestName = 'core-manifest.json'

$Web2Root  = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$SourceDir = Join-Path $Web2Root 'ui\src\core'
$DestDir   = Join-Path $ArRepo 'Frontend\src\core'

if (-not (Test-Path $SourceDir)) { throw "Web2 core not found: $SourceDir" }
if (-not (Test-Path (Join-Path $ArRepo 'Frontend'))) { throw "AR Frontend not found under: $ArRepo" }

<#
  SHA-256 of a file's content with CRLF normalised to LF.
  Why: Git for Windows (core.autocrlf) rewrites line endings on checkout, so
  the same committed file can be LF in one working tree and CRLF in another.
  Hashing raw bytes would then report every core file as "modified in AR".
  All core files are text (TS/TSX/CSS/MD), so normalising is safe.
#>
function Get-NormalizedHash([string]$Path) {
  $bytes = [IO.File]::ReadAllBytes($Path)
  $list = New-Object System.Collections.Generic.List[byte] ($bytes.Length)
  for ($i = 0; $i -lt $bytes.Length; $i++) {
    # Drop the CR of every CRLF pair.
    if ($bytes[$i] -eq 13 -and $i + 1 -lt $bytes.Length -and $bytes[$i + 1] -eq 10) { continue }
    $list.Add($bytes[$i])
  }
  $sha = [Security.Cryptography.SHA256]::Create()
  try { return ([BitConverter]::ToString($sha.ComputeHash($list.ToArray())) -replace '-', '').ToLowerInvariant() }
  finally { $sha.Dispose() }
}

<#
  Returns a hashtable of relative path (forward slashes) -> line-ending
  normalised SHA-256 for every file under $Root, excluding the manifest itself.
#>
function Get-FileHashMap([string]$Root) {
  $map = @{}
  if (-not (Test-Path $Root)) { return $map }
  Get-ChildItem $Root -Recurse -File | ForEach-Object {
    $rel = $_.FullName.Substring($Root.Length + 1).Replace('\', '/')
    if ($rel -ne $ManifestName) {
      $map[$rel] = Get-NormalizedHash $_.FullName
    }
  }
  return $map
}

# 1. Web2 source state: commit hash, and refuse uncommitted core changes.
Push-Location $Web2Root
try {
  $commit = (git rev-parse --short HEAD).Trim()
  $dirty  = git status --porcelain -- 'ui/src/core'
} finally { Pop-Location }
if ($dirty) {
  if (-not $AllowDirty) {
    throw "Web2 ui/src/core has uncommitted changes. Commit them first (or use -AllowDirty).`n$($dirty -join "`n")"
  }
  $commit = "$commit-dirty"
}

$source = Get-FileHashMap $SourceDir
$dest   = Get-FileHashMap $DestDir

# 2. Drift check against the previous manifest in AR.
$manifestPath = Join-Path $DestDir $ManifestName
if (Test-Path $manifestPath) {
  $previous = Get-Content $manifestPath -Raw | ConvertFrom-Json
  $drifted = @()
  foreach ($prop in $previous.files.PSObject.Properties) {
    $rel = $prop.Name
    if ($dest.ContainsKey($rel) -and $dest[$rel] -ne $prop.Value) { $drifted += "modified in AR: $rel" }
    if (-not $dest.ContainsKey($rel)) { $drifted += "deleted in AR:  $rel" }
  }
  foreach ($rel in $dest.Keys) {
    if (-not $previous.files.PSObject.Properties[$rel]) { $drifted += "added in AR:    $rel" }
  }
  if ($drifted.Count -gt 0) {
    $msg = "AR core differs from the last sync (one-way rule: change the core in Web2, not in AR):`n  " + ($drifted -join "`n  ")
    if (-not $Force) { throw "$msg`nUse -Force to overwrite." }
    Write-Warning $msg
  }
}

# 3. Plan the mirror.
$toCopy   = @($source.Keys | Where-Object { -not $dest.ContainsKey($_) -or $dest[$_] -ne $source[$_] } | Sort-Object)
$toDelete = @($dest.Keys   | Where-Object { -not $source.ContainsKey($_) } | Sort-Object)

Write-Host "Web2 core @ $commit -> $DestDir" -ForegroundColor Cyan
Write-Host "  copy/update: $($toCopy.Count)   delete: $($toDelete.Count)   unchanged: $($source.Count - $toCopy.Count)"
$toCopy   | ForEach-Object { Write-Host "  + $_" -ForegroundColor DarkGreen }
$toDelete | ForEach-Object { Write-Host "  - $_" -ForegroundColor DarkYellow }

if ($WhatIf) { Write-Host 'WhatIf: nothing written.' -ForegroundColor Yellow; return }

foreach ($rel in $toCopy) {
  $target = Join-Path $DestDir $rel
  New-Item -ItemType Directory -Force -Path (Split-Path $target -Parent) | Out-Null
  Copy-Item (Join-Path $SourceDir $rel) $target -Force
}
foreach ($rel in $toDelete) {
  Remove-Item (Join-Path $DestDir $rel) -Force
}
# Remove directories left empty by deletions.
if (Test-Path $DestDir) {
  Get-ChildItem $DestDir -Recurse -Directory | Sort-Object { $_.FullName.Length } -Descending |
    Where-Object { -not (Get-ChildItem $_.FullName -Force) } | Remove-Item -Force
}

# 4. Write the manifest (sorted keys, LF line endings, no BOM).
$ordered = [ordered]@{}
foreach ($rel in ($source.Keys | Sort-Object)) { $ordered[$rel] = $source[$rel] }
$manifest = [ordered]@{
  note         = 'Generated by Athena Web 2 scripts/sync-core-to-ar.ps1. Do not edit core files in AR; change them in Web2 and re-sync.'
  sourceRepo   = 'https://github.com/SgtFoose/Athena-Web-2'
  sourceCommit = $commit
  syncedAtUtc  = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
  files        = $ordered
}
$json = ($manifest | ConvertTo-Json -Depth 4) -replace "`r`n", "`n"
[IO.File]::WriteAllText($manifestPath, $json + "`n", (New-Object Text.UTF8Encoding $false))

Write-Host "Done. Review and commit in AR on a branch." -ForegroundColor Green
