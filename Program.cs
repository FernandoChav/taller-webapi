using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Taller1.src.Data;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno
Env.Load();

// Configurar la cadena de conexión
string connectionString = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? "Data Source=app.db";

// Registrar el DbContext en el contenedor de servicios
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
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

app.Run();
