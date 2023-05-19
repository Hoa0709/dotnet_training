using System.Text;
using System.Text.Json.Serialization;
using app.Connects;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    var Services = builder.Services;
    Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));
    Services.AddDbContext<AppIdentityDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityDbContext")));
    Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

    Services.AddControllers();
    Services.AddAutoMapper(typeof(Program));
    Services.AddControllersWithViews();
    //Add service
    Services.AddTransient<IPrograms, ProgramRepository>();
    Services.AddTransient<ILocations, LocationRepository>();
    Services.AddTransient<INews, NewsRepository>();
    Services.AddTransient<IArtists, ArtistRepository>();
    Services.AddTransient<ITicket, TicketRepository>();
    Services.AddTransient<IBookTicket,BookTicketRepository>();
    Services.AddTransient<IAccount,AccountRepository>();
    Services.AddTransient<IManagerAccount,ManagerAccountRepository>();
    Services.AddTransient<IEmailService,EmailService>();

    Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

}
var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        // app.UseExceptionHandler();
        // app.UseHsts();
        app.UseDeveloperExceptionPage();
    }
    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

    app.UseHttpsRedirection();

    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapControllers();
}

app.Run("http://localhost:4000");