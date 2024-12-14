Clear-Host
abp clean
Remove-Item -Force -Recurse -Path .\.vs\
dotnet restore
dotnet build
