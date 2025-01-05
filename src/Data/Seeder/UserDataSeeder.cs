using Bogus;
using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.Util;

namespace Taller1.Data.Seeder;

/// <summary>
/// Seeder class responsible for populating the Users table with initial user data.
/// It adds a predefined administrator user and generates additional random users using Faker.
/// </summary>
public class UserDataSeeder : IDataSeeder<User>
{

    private const int LengthRut = 8;
    private const int QuantityUsers = 50;
    private readonly Random _random = new Random();
    
    private readonly DbSet<User> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IEncryptStrategy _strategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDataSeeder"/> class.
    /// </summary>
    /// <param name="dbContext">The instance of the application database context to interact with the database.</param>
    /// <param name="strategy">The encryption strategy used to encrypt user passwords.</param>
    public UserDataSeeder(ApplicationDbContext dbContext,
        IEncryptStrategy strategy)
    {
        _dbSet = dbContext.Users;
        _strategy = strategy;
        _applicationDbContext = dbContext;
    }

    /// <summary>
    /// Seeds the Users table with initial data if it is empty.
    /// Adds a predefined administrator user and generates additional random users using Faker.
    /// </summary>
    public void Seed()
    {
        if (_dbSet.Any())
        {
            return;
        }
        
        Console.WriteLine("MAKING SEEDER");

        _dbSet.Add(new User
        {
            Name = "Ignacio Mancilla",
            Rut = "204166994",
            Email = "admin@idwm.cl",
            Gender = GenderType.Male,
            Password = _strategy.Encrypt("P4sswOrd"),
            RoleId = 2
        });
        
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Rut, u => Ruts.Create(LengthRut))
            .RuleFor(u => u.Name, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Birthdate, f => f.Date.Recent())
            .RuleFor(u => u.RoleId, f => 0)
            .RuleFor(u => u.Password, f => _strategy.Encrypt(f.Internet.Password()))
            .RuleFor(u => u.Gender, f => GenderType.Other)
            .RuleFor(u => u.RoleId, f => 1);

        userFaker.Generate(QuantityUsers).ForEach(user =>
        {
            _dbSet.Add(user);
        });

        _applicationDbContext.SaveChanges();
    }
    
    /// <summary>
    /// Retrieves the DbSet of Users from the database.
    /// </summary>
    /// <returns>The DbSet representing the Users table.</returns>
    public DbSet<User> Get()
    { 
        return _dbSet;
    }
}