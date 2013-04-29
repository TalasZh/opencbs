C:\dev_factory\Tools\Wix3.0.2925.0\candle -out ..\..\Installer\ ..\Src\WixProject\Octopus.wxs
C:\dev_factory\Tools\Wix3.0.2925.0\light.exe -b ..\..\BuildRelease -out ..\..\Installer\Octopus.msi ..\..\Installer\Octopus.wixobj
copy ..\..\Installer\Octopus.msi ..\..\Installer\FullInstall\Octopus.msi
copy ..\..\Installer\Octopus.msi ..\..\Installer\WithoutSQL\Octopus.msi
pause