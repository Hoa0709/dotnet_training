dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.AspNetCore.Authentication.JwtBearer -v 6.0.16
Microsoft.EntityFrameworkCore.Design
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 6.0.14
AutoMapper.Extensions.Microsoft.DependencyInjection
######
######
dotnet ef migrations add InitialCreate --context AppDbContext --output-dir Migrations/AppDbContextMigrations
dotnet ef migrations add InitialCreate --context AppIdentityDbContext --output-dir Migrations/AppIdentityDbContextMigrations
dotnet ef database update --context AppDbContext
dotnet ef database update --context AppIdentityDbContext
dotnet ef migrations script
dotnet ef migrations remove
#####
git branch (new-branch)
git checkout (new-branch)
git add .
git commit -m "Eden added"
git push origin

