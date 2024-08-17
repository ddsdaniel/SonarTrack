# Migrations

## Add migration
- Open Package Manager Console
- Run: `dotnet ef migrations add InitialCreate --context SonarTrack.Infrastructure.Data.SonarTrackDbContext --output-dir ./Data/Migrations --project ./src/SonarTrack.Infrastructure/SonarTrack.Infrastructure.csproj --startup-project ./src/SonarTrack.WebApi`

## Apply
- Run: `dotnet ef database update --project ./src/SonarTrack.WebApi`