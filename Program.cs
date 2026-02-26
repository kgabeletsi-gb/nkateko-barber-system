using Microsoft.EntityFrameworkCore;
using NkatekoBarberWeb.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var host = Environment.GetEnvironmentVariable("PGHOST");
    var port = Environment.GetEnvironmentVariable("PGPORT");
    var user = Environment.GetEnvironmentVariable("PGUSER");
    var password = Environment.GetEnvironmentVariable("PGPASSWORD");
    var database = Environment.GetEnvironmentVariable("PGDATABASE");

    if (!string.IsNullOrEmpty(host))
    {
        var connectionString =
            $"Host={host};" +
            $"Port={port};" +
            $"Database={database};" +
            $"Username={user};" +
            $"Password={password};" +
            $"SSL Mode=Require;Trust Server Certificate=true";

        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

var app = builder.Build();

// Auto migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

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

var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{portEnv}");

app.Run();