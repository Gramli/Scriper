param(
        [Parameter(Mandatory)] # Positional parameter
        [string]$Path,

        [Parameter(Mandatory)] # Positional parameter
        [string]$Destination
)

Write-Output $Path

Write-Output $Destination

Get-Host | Select-Object Version