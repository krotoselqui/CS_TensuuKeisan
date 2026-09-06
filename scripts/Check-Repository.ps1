$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$repoRoot = Split-Path $PSScriptRoot -Parent
$projectRoot = Join-Path $repoRoot 'MahjongScoreTrainer'
[xml]$project = Get-Content (Join-Path $projectRoot 'MahjongScoreTrainer.csproj') -Raw
$namespace = New-Object System.Xml.XmlNamespaceManager($project.NameTable)
$namespace.AddNamespace('msb', 'http://schemas.microsoft.com/developer/msbuild/2003')
$registered = @($project.SelectNodes('//msb:Compile', $namespace) | ForEach-Object {
    $sourcePath = [IO.Path]::GetFullPath((Join-Path $projectRoot $_.Include))
    if (-not (Test-Path -LiteralPath $sourcePath -PathType Leaf)) {
        throw "Registered source does not exist: $sourcePath"
    }
    $sourcePath
})
if ($registered.Count -eq 0) { throw 'No Compile entries found.' }
if (@($registered | Select-Object -Unique).Count -ne $registered.Count) {
    throw 'Duplicate Compile entries found.'
}

# Limit this contract to application sources. Tests may use a separate project later.
$sources = @(Get-ChildItem -LiteralPath $projectRoot -Filter '*.cs' -File -Recurse |
    Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' } |
    ForEach-Object { $_.FullName })
foreach ($source in $sources) {
    if ($registered -notcontains $source) {
        throw "Source missing from Compile entries: $source"
    }
}

# Check repository-relative Markdown file links, not URLs or heading anchors.
$documents = @((Join-Path $repoRoot 'README.md'), (Join-Path $repoRoot 'AGENTS.md'))
$documents += @(Get-ChildItem (Join-Path $repoRoot 'docs') -Filter '*.md' -File -Recurse |
    ForEach-Object { $_.FullName })
foreach ($document in $documents) {
    $content = Get-Content -LiteralPath $document -Raw -Encoding UTF8
    foreach ($match in [regex]::Matches($content, '\]\(([^)]+)\)')) {
        $target = $match.Groups[1].Value
        if ($target -match '^[a-zA-Z][a-zA-Z0-9+.-]*:' -or $target.StartsWith('#')) { continue }
        $target = [Uri]::UnescapeDataString(($target -split '#', 2)[0])
        if (-not (Test-Path -LiteralPath (Join-Path (Split-Path $document -Parent) $target))) {
            throw "Broken document link in ${document}: $target"
        }
    }
}
Write-Host "PASS: $($sources.Count) application sources registered; $($documents.Count) documents checked."
