using JobBoard.API.Services;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

try
{
    Console.WriteLine("=== APPLICATION STARTING ===");

    builder.Services.AddControllers();

    var connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");

    Console.WriteLine(
        $"Connection String Found: {!string.IsNullOrWhiteSpace(connectionString)}");

    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    Console.WriteLine(
        $"JWT Key Found: {!string.IsNullOrWhiteSpace(jwtKey)}");

    Console.WriteLine(
        $"JWT Issuer Found: {!string.IsNullOrWhiteSpace(jwtIssuer)}");

    Console.WriteLine(
        $"JWT Audience Found: {!string.IsNullOrWhiteSpace(jwtAudience)}");

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new Exception("DefaultConnection missing");

    if (string.IsNullOrWhiteSpace(jwtKey))
        throw new Exception("JWT Key missing");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                };
        });

    builder.Services.AddAuthorization();

    builder.Services.AddScoped<EmailService>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            "AllowAngular",
            policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });

    var app = builder.Build();

    Console.WriteLine("=== APP BUILT ===");

    try
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        Console.WriteLine("=== TESTING DATABASE ===");

        bool canConnect = db.Database.CanConnect();

        Console.WriteLine($"DATABASE CONNECTED: {canConnect}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("=== DATABASE ERROR ===");
        Console.WriteLine(ex.ToString());
        throw;
    }

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    var uploadsPath =
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "Uploads");

    if (!Directory.Exists(uploadsPath))
    {
        Directory.CreateDirectory(uploadsPath);
    }

    app.UseStaticFiles(
        new StaticFileOptions
        {
            FileProvider =
                new PhysicalFileProvider(
                    uploadsPath),
            RequestPath = "/Uploads"
        });

    app.UseCors("AllowAngular");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapGet("/", () =>
        "JobBoard API Running");

    Console.WriteLine(
        "=== APPLICATION STARTED SUCCESSFULLY ===");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("=== STARTUP ERROR ===");
    Console.WriteLine(ex.ToString());
    throw;
}