Remove-Item -Path .\artifacts\ -Recurse -Force -ErrorAction SilentlyContinue
dotnet build .\DKW.Abp.sln --configuration Release --no-restore
dotnet test --configuration Release --no-restore --no-build --verbosity normal
nuget push .\artifacts\*.nupkg -Source 'https://api.nuget.org/v3/index.json' -ApiKey $env:NUGET_API_KEY
