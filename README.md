dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
<!-- dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -->
dotnet add package
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.AspNetCore.Authentication.JwtBearer -v 6.0.16
Microsoft.EntityFrameworkCore.Design
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 6.0.14
######
dotnet ef migrations remove
dotnet ef migrations add createdb
dotnet ef database update
######
dotnet ef migrations add InitialCreate --context AppDbContext --output-dir Migrations/AppDbContextMigrations
dotnet ef migrations add InitialCreate --context AppIdentityDbContext --output-dir Migrations/AppIdentityDbContextMigrations
dotnet ef database update --context AppDbContext
dotnet ef database update --context AppIdentityDbContext

