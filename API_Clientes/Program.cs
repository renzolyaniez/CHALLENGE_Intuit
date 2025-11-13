using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository;
using API_Clientes.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<Contexto>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped(typeof(ICliente), typeof(ClienteRepository));

var app = builder.Build();

// Registrar el middleware de captura LO ANTES POSIBLE en la tubería:
app.UseExceptionLogging();

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
