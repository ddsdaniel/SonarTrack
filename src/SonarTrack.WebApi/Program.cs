using Serilog;
using Serilog.Filters;
using SonarTrack.Infrastructure.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Filter.ByExcluding(Matching.FromSource("System.Net.Http.HttpClient"))
            .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDependencyInjections(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

await Log.CloseAndFlushAsync();