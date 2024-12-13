abp clean
Get-ChildItem .\ -Recurse -Include *Module.cs | Update-DependsOn
Get-ChildItem .\ -Recurse -Include *.csproj | Remove-CommonProps
Remove-Item -Force -Recurse .\.vs
Get-ChildItem .\ -Recurse -Include *.csproj -Exclude .\.github\** | Where-Object fullname | ForEach-Object { dotnet sln add $_.fullname }
