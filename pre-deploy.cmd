dotnet restore

dotnet clean --configuration Debug
dotnet clean --configuration Release

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\test\TauCode.Cqrs.Tests\TauCode.Cqrs.Tests.csproj
dotnet test -c Release .\test\TauCode.Cqrs.Tests\TauCode.Cqrs.Tests.csproj

nuget pack nuget\TauCode.Cqrs.nuspec