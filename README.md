# Задаем URL для загрузки установщика Python
$pythonInstallerUrl = "https://www.python.org/ftp/python/3.10.0/python-3.10.0-amd64.exe"
$pythonInstallerPath = "$env:TEMP\python-installer.exe"

# Задаем URL для загрузки установщика Git
$gitInstallerUrl = "https://github.com/git-for-windows/git/releases/latest/download/Git-x86_64.exe"
$gitInstallerPath = "$env:TEMP\git-installer.exe"

# Задаем URL для загрузки установщика AnyDesk
$anydeskInstallerUrl = "https://download.anydesk.com/AnyDesk.exe"
$anydeskInstallerPath = "$env:TEMP\AnyDesk.exe"

# Задаем URL для загрузки установщика Albion Online
$albionInstallerUrl = "https://albiononline.com/en/download"
$albionInstallerPath = "$env:TEMP\AlbionOnlineInstaller.exe"

# Задаем URL для загрузки установщика Amnesia VPN
$amnesiaVpnInstallerUrl = "https://amnesiavpn.com/download/AmnesiaVPN.exe"
$amnesiaVpnInstallerPath = "$env:TEMP\AmnesiaVPN.exe"

# Задаем URL для загрузки установщика Npcap
$npcapInstallerUrl = "https://nmap.org/npcap/dist/npcap-1.75.exe"
$npcapInstallerPath = "$env:TEMP\npcap-installer.exe"

# Функция для установки программ и вывода сообщения
function Install-Software {
    param (
        [string]$installerPath,
        [string]$installArgs,
        [string]$softwareName
    )

    Start-Process -FilePath $installerPath -ArgumentList $installArgs -Wait
    Write-Host "$softwareName успешно установлен!" -ForegroundColor Green
}

# Загружаем и устанавливаем Python
Invoke-WebRequest -Uri $pythonInstallerUrl -OutFile $pythonInstallerPath
Install-Software -installerPath $pythonInstallerPath -installArgs '/quiet InstallAllUsers=1 PrependPath=1' -softwareName 'Python'

# Загружаем и устанавливаем Git
Invoke-WebRequest -Uri $gitInstallerUrl -OutFile $gitInstallerPath
Install-Software -installerPath $gitInstallerPath -installArgs '/VERYSILENT /NORESTART' -softwareName 'Git'

# Загружаем и устанавливаем AnyDesk
Invoke-WebRequest -Uri $anydeskInstallerUrl -OutFile $anydeskInstallerPath
Install-Software -installerPath $anydeskInstallerPath -installArgs '/install /quiet' -softwareName 'AnyDesk'

# Загружаем и устанавливаем Albion Online
Invoke-WebRequest -Uri $albionInstallerUrl -OutFile $albionInstallerPath
Start-Process -FilePath $albionInstallerPath -Wait
Write-Host "Albion Online успешно установлен!" -ForegroundColor Green

# Загружаем и устанавливаем Amnesia VPN
Invoke-WebRequest -Uri $amnesiaVpnInstallerUrl -OutFile $amnesiaVpnInstallerPath
Start-Process -FilePath $amnesiaVpnInstallerPath -Wait
Write-Host "Amnesia VPN успешно установлен!" -ForegroundColor Green

# Загружаем и устанавливаем Npcap
Invoke-WebRequest -Uri $npcapInstallerUrl -OutFile $npcapInstallerPath
Start-Process -FilePath $npcapInstallerPath -ArgumentList '/S' -Wait
Write-Host "Npcap успешно установлен!" -ForegroundColor Green

# Удаляем установщики после завершения установки
Remove-Item $pythonInstallerPath
Remove-Item $gitInstallerPath
Remove-Item $anydeskInstallerPath
Remove-Item $albionInstallerPath
Remove-Item $amnesiaVpnInstallerPath
Remove-Item $npcapInstallerPath

# Проверяем установку Python и Git
python --version
git --version
