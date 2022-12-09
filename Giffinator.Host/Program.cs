using System.Configuration;
using Giffinator.Host.Data;
using Giffinator.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SqliteConnection>(_=> new SqliteConnection(builder.Configuration.GetConnectionString("GiffinatorSqlLite")));
builder.Services.AddTransient<SqliteDbSetup>();
var config = new MastodonConfig();
builder.Configuration.GetSection(MastodonConfig.Section).Bind(config);
builder.Services.Configure<MastodonConfig>(builder.Configuration.GetSection(MastodonConfig.Section));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/thumbnails", ()=> "Hello"
).WithName("thumbnails");
app.MapControllers();
await using (var scope = app.Services.CreateAsyncScope())
{
    T Service<T>() where T : notnull
    {
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    Service<ILogger<Program>>().LogInformation("Path: {Path}", AppContext.BaseDirectory);

    var dbSetup = Service<SqliteDbSetup>();
    await dbSetup.EnsureDbSetup();
}

app.Run();