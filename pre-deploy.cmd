dotnet restore

dotnet build TauCode.Cqrs.sln -c Debug
dotnet build TauCode.Cqrs.sln -c Release

dotnet test TauCode.Cqrs.sln -c Debug
dotnet test TauCode.Cqrs.sln -c Release

nuget pack nuget\TauCode.Cqrs.nuspec