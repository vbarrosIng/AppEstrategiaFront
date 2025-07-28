using ApiEstrategias.Api.Middleware;
using ApiEstrategias.Application.Interfaces;
using ApiEstrategias.Application.Services;
using ApiEstrategias.Domain.Entities;
using ApiEstrategias.Domain.Interfaces;
using ApiEstrategias.Infrastructure.Data;
using ApiEstrategias.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddScoped<INeumaticoRepository, NeumaticoRepository>();
builder.Services.AddScoped<ILogsRepository, LogsRepository>();
builder.Services.AddScoped<IEstrategiasRepository, EstrategiaRepository>();
builder.Services.AddScoped<IPilotoRepository, PilotoRepository>();


//Services
builder.Services.AddScoped<IEstrategiaService, EstrategiaService>();
builder.Services.AddScoped<ILogService, LogService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConecction");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAngularLocalhost");

app.UseMiddleware<MiddlewareSeCretKey>();

app.MapControllers();

app.Run();
