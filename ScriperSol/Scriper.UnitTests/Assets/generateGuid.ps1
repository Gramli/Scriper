$g = [guid]::NewGuid()
Set-Clipboard -Value $g
$date = Get-Date
$name = [System.String]::Concat($date.ToString("yyyy-MM-dd HHmmss"), ".txt")
New-Item -Path "C:\temp" -Name $name -ItemType "file" -Value $g