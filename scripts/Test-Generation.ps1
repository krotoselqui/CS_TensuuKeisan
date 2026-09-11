param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug'
)
$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path $PSScriptRoot -Parent
$scratch = Join-Path $env:TEMP ('mahjong-generation-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $scratch | Out-Null
[xml]$project = Get-Content (Join-Path $repoRoot 'MahjongScoreTrainer/MahjongScoreTrainer.csproj') -Raw
$namespace = New-Object Xml.XmlNamespaceManager($project.NameTable)
$namespace.AddNamespace('msb', 'http://schemas.microsoft.com/developer/msbuild/2003')
$sources = @()
foreach ($entry in $project.SelectNodes('//msb:Compile', $namespace)) {
    $path = Join-Path $repoRoot "MahjongScoreTrainer/$($entry.Include)"
    if ($entry.Include -eq 'Program.cs' -or $entry.Include -eq 'ConsoleProgress.cs') {
        $adapted = Join-Path $scratch $entry.Include
        $text = "using Console = TestConsole;`r`n" + [IO.File]::ReadAllText($path)
        [IO.File]::WriteAllText($adapted, $text, (New-Object Text.UTF8Encoding($false)))
        $sources += $adapted
    }
    else { $sources += $path }
}
$compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$flags = @('/optimize+')
if ($Configuration -eq 'Debug') { $flags = @('/debug+', '/define:DEBUG,TRACE') }
& $compiler /nologo /warn:0 /target:exe /main:GenerationChecks "/out:$scratch/checks.exe" @flags @sources (Join-Path $repoRoot 'tests/GenerationChecks.cs')
if ($LASTEXITCODE -ne 0) { throw 'Generation check compilation failed.' }
& (Join-Path $scratch 'checks.exe') (Join-Path $repoRoot 'tests/Fixtures') $scratch
if ($LASTEXITCODE -ne 0) { throw "Generation checks failed. Outputs: $scratch" }
Write-Host "Generation check outputs: $scratch"
