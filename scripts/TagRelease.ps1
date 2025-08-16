<#
.SYNOPSIS
Creates and pushes a Semantic Version (SemVer) tag (e.g. v1.2.3 or v1.2.3-beta, v1.2.3-beta.4).

.DESCRIPTION
- Uses Nerdbank.GitVersioning (nbgv) to obtain the current version.
- Default tag: v + SimpleVersion (no git height/hash metadata).
- Optional prerelease automation: supply -Prerelease <label> to produce v<base>-<label> or with -AutoIncrementPrerelease to produce v<base>-<label>.N (N auto-incremented).
- You can override the base version with -BaseVersion (otherwise the current SimpleVersion's base numeric portion is used).
- If -IncludeMetadata is specified (and no -Prerelease), uses full SemVer2 (may include git metadata).
- Generates simple release notes (commit subjects since previous v* tag).
- Creates annotated tag and pushes to origin.
- Outputs tag name for pipeline usage.

.PARAMETER DryRun
Show the tag that would be created and notes, but does not create/push.

.PARAMETER NotesFile
Optional path to write release notes separately.

.PARAMETER Force
Recreate (force update) the tag if it already exists.

.PARAMETER IncludeMetadata
Use full SemVer2 (including git height/commit metadata) instead of SimpleVersion (ignored if -Prerelease used).

.PARAMETER Prerelease
Prerelease label (e.g. beta, rc). When provided the tag will be prerelease style.

.PARAMETER AutoIncrementPrerelease
When used with -Prerelease, auto-increments a numeric suffix (.1, .2, ...) based on existing tags for same base version & label.

.PARAMETER BaseVersion
Override the base numeric version (e.g. 2.2.0) when creating prerelease tags.

.EXAMPLE
./TagRelease.ps1
.EXAMPLE
./TagRelease.ps1 -Prerelease beta -AutoIncrementPrerelease
.EXAMPLE
./TagRelease.ps1 -Prerelease rc -BaseVersion 2.2.0 -AutoIncrementPrerelease -Force
#>

[CmdletBinding()]
param(
    [switch]$DryRun,
    [string]$NotesFile,
    [switch]$Force,
    [switch]$IncludeMetadata,
    [string]$Prerelease,
    [switch]$AutoIncrementPrerelease,
    [string]$BaseVersion,
    # Deprecated (backward compatibility). Ignored.
    [int]$ForceNumber
)

$git = Get-Command git.exe -CommandType Application -ErrorAction SilentlyContinue
if (-not $git) { throw "Git executable not found. Ensure git is installed and in PATH." }

function Exec {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true, Position=0, ValueFromRemainingArguments=$true)]
        [string[]]$CmdAndArgs
    )
    $psiOut = & $git @CmdAndArgs 2>&1
    $code = $LASTEXITCODE
    if ($code -ne 0) {
        throw ("Git command failed (exit {0}): git {1}`n{2}" -f $code, ($CmdAndArgs -join ' '), $psiOut)
    }
    ($psiOut -join "`n").TrimEnd()
}

function Get-NbgvVersionObject {
    $nbgv = Get-Command nbgv -ErrorAction SilentlyContinue
    $json = $null
    if ($nbgv) {
        $json = & $nbgv get-version --format json 2>$null
    } else {
        $dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
        if (-not $dotnet) { throw "Neither nbgv nor dotnet (for 'dotnet nbgv') found on PATH." }
        $json = & $dotnet nbgv get-version --format json 2>$null
    }
    if (-not $json) { throw "Failed to obtain version information from Nerdbank.GitVersioning (nbgv)." }
    try { return $json | ConvertFrom-Json } catch { throw "Failed to parse nbgv JSON output. Raw: $json" }
}

# Verify repo
try { Exec rev-parse --is-inside-work-tree | Out-Null } catch { throw "Not a git repository (or cannot access .git). $_" }

# Warn if dirty (informational only)
$status = Exec status --porcelain
if ($status) { Write-Warning "Working tree not clean." }

# Obtain version from nbgv
$versionInfo = Get-NbgvVersionObject

# Determine base version/tag
$tag = $null
if ($Prerelease) {
    if ($IncludeMetadata) { Write-Warning "-IncludeMetadata ignored because -Prerelease specified." }
    # Determine base numeric version
    $numericBase = if ($BaseVersion) { $BaseVersion } else {
        # Use SimpleVersion's numeric portion (strip any existing pre-release)
        $sv = $versionInfo.SimpleVersion
        if (-not $sv) { throw "nbgv did not return SimpleVersion needed for prerelease computation." }
        if ($sv -match '^(\d+\.\d+\.\d+)') { $matches[1] } else { throw "Unable to extract numeric base from SimpleVersion '$sv'" }
    }

    $prLabel = $Prerelease.TrimEnd('.')
    if ([string]::IsNullOrWhiteSpace($prLabel)) { throw "Prerelease label cannot be empty." }

    $suffix = ''
    if ($AutoIncrementPrerelease) {
        $existingRaw = Exec tag --list "v$numericBase-$prLabel.*" 2>$null
        $nextN = 1
        if ($existingRaw) {
            $nums = @()
            foreach ($t in ($existingRaw -split "`n")) {
                if ($t -match "^v$([Regex]::Escape($numericBase))-$([Regex]::Escape($prLabel))\.(\d+)$") { $nums += [int]$matches[1] }
            }
            if ($nums.Count -gt 0) { $nextN = ([int]($nums | Measure-Object -Maximum).Maximum) + 1 }
        }
        $suffix = ".$nextN"
    }
    $tag = "v$numericBase-$prLabel$suffix"
} else {
    $baseVersion = if ($IncludeMetadata) { $versionInfo.SemVer2 } else { $versionInfo.SimpleVersion }
    if (-not $baseVersion) { throw "nbgv did not return required version field (SimpleVersion/SemVer2)." }
    $tag = "v$baseVersion"
}

Write-Host "Proposed tag: $tag"

# Determine previous semantic tag
$allSemVerTagsRaw = Exec tag --list "v*"
$prevTag = $null
if ($allSemVerTagsRaw) {
    $prevTag = ($allSemVerTagsRaw -split "`n" | Where-Object { $_ -and $_ -ne $tag }) | ForEach-Object { $_ }
    if ($prevTag) {
        $sorted = (Exec tag --sort=-creatordate --list "v*") -split "`n" | Where-Object { $_ -and $_ -ne $tag }
        $prevTag = $sorted | Select-Object -First 1
    }
}
Write-Host ("Previous tag: {0}" -f ($prevTag ? $prevTag : "(none)"))

# Generate release notes
if ($prevTag) {
    $log = Exec log --pretty=format:%h` %s "$prevTag..HEAD"
} else {
    $log = Exec log --pretty=format:%h` %s
}
$now = Get-Date -AsUTC
$sourceInfo = if ($Prerelease) { "Prerelease automation" } else { "Nerdbank.GitVersioning (" + ($IncludeMetadata ? 'SemVer2' : 'SimpleVersion') + ")" }
$notesHeader = "Release $tag`nDate: $($now.ToString('u'))`nVersion Source: $sourceInfo"
$notesBody = if ($log) { $log } else { "No commits found." }
$releaseNotes = "$notesHeader`n`nCommits:`n$notesBody`n"

if ($DryRun) {
    Write-Host "`n[DryRun] Release Notes:`n$releaseNotes"
    if ($NotesFile) { $releaseNotes | Out-File -FilePath $NotesFile -Encoding UTF8 }
    return
}

# Check existing tag
$tagExists = $false
if ($allSemVerTagsRaw) { $tagExists = ($allSemVerTagsRaw -split "`n") -contains $tag }
if ($tagExists -and -not $Force) { throw "Tag $tag already exists. Use -Force to recreate it (will move the tag)." }

$tempFile = New-TemporaryFile
try {
    $tagMessage = "$tag`n`n$releaseNotes"
    Set-Content -Path $tempFile -Value $tagMessage -Encoding UTF8

    if ($tagExists -and $Force) {
        Exec tag -d $tag
        try { Exec push origin :refs/tags/$tag } catch { Write-Warning "Failed to delete remote tag $tag (may not exist remotely)." }
    }

    Exec tag -a $tag -F $tempFile.FullName
    Exec push origin $tag
    Write-Host "Tag $tag created & pushed."
} finally {
    if (Test-Path $tempFile) { Remove-Item $tempFile -Force -ErrorAction SilentlyContinue }
}

if ($NotesFile) {
    $releaseNotes | Out-File -FilePath $NotesFile -Encoding UTF8
    Write-Host "Release notes written to $NotesFile"
}

# GitHub Actions compatibility output
Write-Output "::set-output name=tag::$tag"
# if ($env:GITHUB_OUTPUT) { Add-Content -Path $env:GITHUB_OUTPUT -Value "tag=$tag" }