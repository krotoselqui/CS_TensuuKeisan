param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug'
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

# Windows PowerShell uses .NET Framework, matching the application runtime.
if ($PSVersionTable.PSEdition -ne 'Desktop') {
    throw 'Run this script with Windows PowerShell (powershell.exe), not pwsh.'
}
$repoRoot = Split-Path $PSScriptRoot -Parent
$assemblyPath = Join-Path $repoRoot "ConsoleApplication9/bin/$Configuration/ConsoleApplication9.exe"
$assembly = [Reflection.Assembly]::LoadFrom($assemblyPath)
$scoreType = $assembly.GetType('ConsoleApplication9.SCORE_DATA', $true)
$constructor = $scoreType.GetConstructor([type[]]@([int], [int], [bool], [bool]))
if ($null -eq $constructor) { throw 'SCORE_DATA constructor not found.' }

function Assert-Equal($Actual, $Expected, [string]$Label) {
    if ($Actual -ne $Expected) { throw "${Label}: expected $Expected, got $Actual" }
}

# Explicit payment fixtures; do not derive expectations using production formulas.
# Columns: name, han/multiplier, fu, yakuman, dealer, ron, non-dealer pays, dealer pays.
$scoreCases = @(
    @('child 30fu 1han', 1, 30, $false, $false, 1000, 300, 500),
    @('dealer 30fu 1han', 1, 30, $false, $true, 1500, 500, 0),
    @('child no kiriage', 4, 30, $false, $false, 7700, 2000, 3900),
    @('child mangan', 5, 30, $false, $false, 8000, 2000, 4000),
    @('dealer mangan', 5, 30, $false, $true, 12000, 4000, 0),
    @('child counted yakuman', 13, 30, $false, $false, 32000, 8000, 16000),
    @('child double yakuman', 2, 0, $true, $false, 64000, 16000, 32000)
)
foreach ($case in $scoreCases) {
    $score = $constructor.Invoke([object[]]@($case[1], $case[2], $case[3], $case[4]))
    Assert-Equal ($scoreType.GetField('scoresum').GetValue($score)) $case[5] "$($case[0]) ron"
    Assert-Equal ($scoreType.GetField('other_pay').GetValue($score)) $case[6] "$($case[0]) other payment"
    Assert-Equal ($scoreType.GetField('dealer_pay').GetValue($score)) $case[7] "$($case[0]) dealer payment"
}

# Access existing internal/private types without changing production visibility for CI.
$programType = $assembly.GetType('ConsoleApplication9.Program', $true)
$conversion = $programType.GetMethod('XBtoXS', [Reflection.BindingFlags]'Static, NonPublic')
if ($null -eq $conversion) { throw 'Tile ID conversion method not found.' }
$tileCases = @(@(0, 1), @(3, 1), @(4, 2), @(135, 34), @(-1, 0), @(136, 0))
foreach ($case in $tileCases) {
    $actual = $conversion.Invoke($null, [object[]]@($case[0]))
    Assert-Equal $actual $case[1] "tile ID $($case[0])"
}

Write-Host "PASS: $($scoreCases.Count) scoring cases and $($tileCases.Count) tile conversion cases."
