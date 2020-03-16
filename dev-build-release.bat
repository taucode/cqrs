dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\tests\TauCode.Cqrs.Tests\TauCode.Cqrs.Tests.csproj
dotnet test -c Release .\tests\TauCode.Cqrs.Tests\TauCode.Cqrs.Tests.csproj

nuget pack nuget\TauCode.Cqrs.nuspec
