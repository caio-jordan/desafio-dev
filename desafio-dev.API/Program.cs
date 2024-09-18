
using desafio_dev.API.Core.IoC;
using desafio_dev.API.Infrastructure.Context;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInitServices();


var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PrevisaoDbContext>(options =>
options.UseNpgsql(conn));

var app = builder.Build();



app.UseHangfireDashboard();

ServiceCollectionExtensions.StartHangFire(builder.Services);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


