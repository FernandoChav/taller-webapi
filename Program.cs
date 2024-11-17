using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Taller1.Authenticate;
using Taller1.Authenticate.Token;
using Taller1.Data;
using Taller1.Data.Seeder;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Service;
using Taller1.src.Models;
using Taller1.Update;
using Taller1.Update.Model;
using Taller1.Util;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

void InstallServices(params ServiceMember[] members)
{
    foreach (var member in members)
    {
        builder.Services.AddScoped(member.ServiceType,
            member.ImplementationType);
    }
}

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_PATH") ?? "Data Source=app.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
    options.EnableSensitiveDataLogging();
}
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

InstallServices(
    ServiceMember.NewInstance<IObjectRepository<User>, UserRepository>(),
    ServiceMember.NewInstance<IImageService, CloudinaryImageService>(),
    ServiceMember.NewInstance<IMapperFactory, MapperFactory>(),
    ServiceMember.NewInstance<IUpdateModel<Product>, ProductEditModel>(),
    ServiceMember.NewInstance<IUpdateModel<User>, UserEditModel>(),
    ServiceMember.NewInstance<IUpdateModel<Role>, RoleEditModel>(),
    ServiceMember.NewInstance<IObjectRepository<Product>, ProductRepository>(),
    ServiceMember.NewInstance<IObjectRepository<Voucher>, VoucherRepository>(),
    ServiceMember.NewInstance<IUserTokenProvider, JwtUserTokenProvider>(),
    ServiceMember.NewInstance<IEncryptStrategy, BcryptEncryptStrategy>(),
    ServiceMember.NewInstance<IAuthenticatorHandler, DefaultAuthenticatorHandler>(),
    ServiceMember.NewInstance<IRegistrationHandler, DefaultRegistrationHandler>(),
    ServiceMember.NewInstance<IObjectRepository<Role>, RoleRepository>(),
    ServiceMember.NewInstance<IDataSeeder<User>, UserDataSeeder>(),
    ServiceMember.NewInstance<IDataSeeder<Role>, RoleDataSeeder>(),
    ServiceMember.NewInstance<IDataSeeder<Product>, ProductDataSeeder>()
);

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

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<CloudinaryImageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();