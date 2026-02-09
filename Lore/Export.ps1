<#
.SYNOPSIS
Concatenate all Markdown files under the `Lore` folder into a single Markdown file.
#>

Param(
    [string]$OutFile = "FullLore.md"
)

function Get-LoreFiles {
    param(
        [string]$Root
    )

    return Get-ChildItem -Path $Root -Recurse -File -Filter *.md | Sort-Object FullName
}

function Get-RelativePath {
    param(
        [string]$FullPath,
        [string]$RepoRoot
    )
    if ($FullPath.StartsWith($RepoRoot)) { return $FullPath.Substring($RepoRoot.Length + 1) }
    return $FullPath
}

function Exclude-OutputFile {
    param(
        [array]$Files,
        [string]$OutFile
    )
    $outFull = $null
    try {
        $rp = Resolve-Path -LiteralPath $OutFile -ErrorAction SilentlyContinue
        if ($rp) { $outFull = $rp.Path }
    } catch { }
    if ($outFull) { return $Files | Where-Object { $_.FullName -ne $outFull } }
    return $Files
}

function Get-SearchRoot {
    param(
        [string]$Subdir
    )
    return Join-Path (Get-Location) $Subdir
}

function Append-FileToOut {
    param(
        [System.IO.FileInfo]$File,
        [string]$OutFile,
        [string]$RepoRoot
    )
    if (-not $File) { return }
    $rel = Get-RelativePath -FullPath $File.FullName -RepoRoot $RepoRoot
    "`n<!-- Source: $rel -->`n" | Out-File -FilePath $OutFile -Append -Encoding Utf8
    # Read the source file as UTF-8 to preserve characters like em-dash correctly on Windows PowerShell
    Get-Content -Raw -Encoding UTF8 -Path $File.FullName | Out-File -FilePath $OutFile -Append -Encoding Utf8
}

function Write-Combined {
    param(
        [array]$Files,
        [string]$OutFile
    )

    # Start fresh
    if (Test-Path $OutFile) { Remove-Item -LiteralPath $OutFile -Force }

    # Ensure file is created with UTF-8 BOM so editors like Notepad detect the encoding correctly.
    $bom = [System.Text.Encoding]::UTF8.GetPreamble()
    [System.IO.File]::WriteAllBytes($OutFile, $bom)

    $repoRoot = (Get-Location).Path
    foreach ($f in $Files) {
        Append-FileToOut -File $f -OutFile $OutFile -RepoRoot $repoRoot
    }
}

function Main {
    param(
        [string]$OutFile = $script:OutFile
    )

    $searchRoot = Get-SearchRoot -Subdir ""
    $files = Get-LoreFiles -Root $searchRoot
    $files = Exclude-OutputFile -Files $files -OutFile $OutFile

    if (-not $files -or $files.Count -eq 0) {
        Write-Warning "No Markdown files found under '$searchRoot'."
        exit 1
    }

    Write-Combined -Files $files -OutFile $OutFile
    Write-Host "Wrote $($files.Count) files to '$OutFile'." -ForegroundColor Green
}

# Call main
Main -OutFile $OutFile
