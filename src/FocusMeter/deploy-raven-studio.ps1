if ($args.Length -ne 2) { exit; }

$packagesFolderPath = Join-Path -path $args[0] -childpath "packages"

$ravenFolders = @(Get-ChildItem $packagesFolderPath | Where-Object{$_.name -like "RavenDB.Database*"})
if ($ravenFolders.Count -eq 0) { exit }

$sourcePath = Join-Path -path $ravenFolders[0].fullname -childpath "lib\net40\Raven.Studio.xap"
$destPath = Join-Path -path $args[1] -childpath "Raven.Studio.xap"

Copy-Item $sourcePath $destPath