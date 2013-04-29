cd ..\..\Installer\WithoutSQL
"c:\Program Files\WinRAR\Rar.exe" a -sfx OctopusWithoutSQL.exe octopus.msi  CrystalReportsR2\Crystal_Reports_V11R2_for_OMFS_v1.0.9_and_over.msi setup.exe
REM "c:\program files\winrar\winrar.exe" a -sfx -iicon..\..\src\Batches\installer.ico OctopusWithoutSQL.exe setup.exe
"c:\Program Files\WinRAR\Rar.exe" c -z..\..\src\Batches\buildsfx.config OctopusWithoutSQL.exe 
