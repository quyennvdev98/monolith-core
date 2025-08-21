//create migration for core schema

dotnet ef migrations add Init -o .\Monolith.Core.Infrastructure\Persistence\Migrations\CoreDb --startup-project .\Monolith.Core.WebAPI\Monolith.Core.WebAPI.csproj --context CoreDbContext --project .\Monolith.Core.Infrastructure\Monolith.Core.Infrastructure.csproj

//Apply migration for core schema
dotnet ef database update --startup-project .\Monolith.Core.WebAPI\Monolith.Core.WebAPI.csproj --context CoreDbContext --project .\Monolith.Core.Infrastructure\Monolith.Core.Infrastructure.csproj