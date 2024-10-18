using System.Text;
using DotNetEnv;
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

void InstallServices(params ServiceMember[] members)
{
    foreach (var member in members)
    {
        builder.Services.AddScoped(member.ServiceType,
            member.ImplementationType);
    }
}

// Cargar las variables de entorno
Env.Load();

// Configurar la cadena de conexión
var connectionString = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? "Data Source=app.db";

// Registrar el DbContext en el contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

InstallServices(
    ServiceMember.NewInstance<IObjectRepository<User>, UserRepository>(),
    ServiceMember.NewInstance<IObjectRepository<Product>, ProductDbSetObjectRepository>(),
    ServiceMember.NewInstance<IUserTokenProvider, JwtUserTokenProvider>(),
    ServiceMember.NewInstance<IEncryptStrategy, BcryptEncryptStrategy>(),
    ServiceMember.NewInstance<ImageService, ImageService>(),
    ServiceMember.NewInstance<IAuthenticatorHandler, DefaultAuthenticatorHandler>(),
    ServiceMember.NewInstance<IRegistrationHandler, DefaultRegistrationHandler>(),
    ServiceMember.NewInstance<IObjectRepository<Role>, RoleRepository>(),
    ServiceMember.NewInstance<IDataSeeder<User>, UserDataSeeder>(),
    ServiceMember.NewInstance<IDataSeeder<Role>, RoleDataSeeder>(),
    ServiceMember.NewInstance<IDataSeeder<Product>, ProductDataSeeder>()
);

/*

builder.Services.AddScoped<IObjectRepository<User>, UserRepository>();
builder.Services.AddScoped<IObjectRepository<Product>, ProductDbSetObjectRepository>();
builder.Services.AddScoped<IUserTokenProvider, JwtUserTokenProvider>();
builder.Services.AddScoped<IEncryptStrategy, BcryptEncryptStrategy>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthenticatorHandler, DefaultAuthenticatorHandler>();
builder.Services.AddScoped<IRegistrationHandler, DefaultRegistrationHandler>();
builder.Services.AddScoped<IObjectRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IDataSeeder<User>, UserDataSeeder>();
builder.Services.AddScoped<IDataSeeder<Role>, RoleDataSeeder>();
builder.Services.AddScoped<IDataSeeder<Product>, ProductDataSeeder>();*/

var jwtSecret = builder.Configuration["JWT:Secret"];
if (jwtSecret == null)
{
    return;
}

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
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