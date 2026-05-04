param(
  [Parameter(Mandatory = $true)]
  [string]$WorkshopId,

  [string]$ContentPath = ".",

  [string]$ChangeNoteFile = ".\workshop-change-note-0.1.0.txt"
)

$publisherCmd = "C:\Program Files (x86)\Steam\steamapps\common\Arma 3 Tools\Publisher\PublisherCmd.exe"

if (-not (Test-Path $publisherCmd)) {
  throw "PublisherCmd.exe not found at: $publisherCmd"
}

if (-not (Test-Path $ContentPath)) {
  throw "Content path not found: $ContentPath"
}

if (-not (Test-Path $ChangeNoteFile)) {
  throw "Change note file not found: $ChangeNoteFile"
}

$resolvedContentPath = (Resolve-Path $ContentPath).Path
$resolvedChangeNoteFile = (Resolve-Path $ChangeNoteFile).Path

Write-Host "Updating Arma 3 Workshop item $WorkshopId from $resolvedContentPath" -ForegroundColor Cyan
& $publisherCmd update "/id:$WorkshopId" "/changeNoteFile:$resolvedChangeNoteFile" "/path:$resolvedContentPath"

if ($LASTEXITCODE -ne 0) {
  throw "PublisherCmd failed with exit code $LASTEXITCODE"
}

Write-Host "Workshop update completed successfully." -ForegroundColor Green
