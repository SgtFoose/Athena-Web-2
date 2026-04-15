$addonBuilder = "C:\Program Files (x86)\Steam\steamapps\common\Arma 3 Tools\AddonBuilder\AddonBuilder.exe"

if (-not (Test-Path $addonBuilder)) {
  throw "AddonBuilder.exe not found at: $addonBuilder"
}

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$source = Join-Path $root "addons_src\athenaweb2_stub"
$output = Join-Path $root "addons"

if (-not (Test-Path $source)) {
  throw "Stub source not found: $source"
}

if (-not (Test-Path $output)) {
  New-Item -ItemType Directory -Path $output | Out-Null
}

Write-Host "Building stub PBO from: $source" -ForegroundColor Cyan
& $addonBuilder $source $output

if ($LASTEXITCODE -ne 0) {
  throw "AddonBuilder failed with exit code $LASTEXITCODE"
}

Write-Host "Stub PBO build complete. Output folder: $output" -ForegroundColor Green
