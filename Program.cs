using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Taller1.Authenticate;
using Taller1.Authenticate.Token;
using Taller1.Data;
using Taller1.Data.Seeder;
using Taller1.Model;
using Taller1.Service;
using Taller1.src.Models;
using Taller1.Util;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno
Env.Load();

// Configurar la cadena de conexión
var connectionString = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? "Data Source=app.db";

// Registrar el DbContext en el contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IObjectRepository<User>, UserRepository>();
builder.Services.AddScoped<IObjectRepository<Product>, ProductDbSetObjectRepository>();
builder.Services.AddScoped<IUserTokenProvider, JwtUserTokenProvider>();
builder.Services.AddScoped<IEncryptStrategy, BcryptEncryptStrategy>();
builder.Services.AddScoped<IAuthenticatorHandler, DefaultAuthenticatorHandler>();
builder.Services.AddScoped<IRegistrationHandler, DefaultRegistrationHandler>();
builder.Services.AddScoped<IObjectRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IDataSeeder<User>, UserDataSeeder>();
builder.Services.AddScoped<IDataSeeder<Role>, RoleDataSeeder>();
builder.Services.AddScoped<IDataSeeder<Product>, ProductDataSeeder>();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        };
    });

builder.Services.AddAuthorization();

// Configurar la sección de Cloudinary desde appsettings.json
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<ImageService>();

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

using (var scope = app.Services.CreateScope())
{
    
    var services = scope.ServiceProvider;
    var roleDataSeeder = services.GetRequiredService<IDataSeeder<Role>>();
    var userDataSeeder = services.GetRequiredService<IDataSeeder<User>>();
    var productDataSeeder = services.GetRequiredService<IDataSeeder<Product>>();
    
    roleDataSeeder.Seed();
    userDataSeeder.Seed();
    productDataSeeder.Seed();

}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();