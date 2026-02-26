using Microsoft.EntityFrameworkCore;
using NkatekoBarberWeb.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    if (!string.IsNullOrEmpty(databaseUrl))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = uri.AbsolutePath.Trim('/'),
            SslMode = SslMode.Require
        }.ToString();

        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();


// 🔥 SAFE DATABASE CREATION (NO CRASH)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database error: {ex.Message}");
    }
}


// 🔥 Railway Port Configuration
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();