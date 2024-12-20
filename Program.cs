using Planificador_Fabrica.Data;
using Microsoft.EntityFrameworkCore;
using Planificador_Fabrica.Models;
using Planificador_Fabrica.Services;
using Planificador_Fabrica.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<IOrdenFabricacionRepository, OrdenFabricacionRepository>();
builder.Services.AddScoped<VRptOrdenFabricacionRepository>();
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

app.Run();
