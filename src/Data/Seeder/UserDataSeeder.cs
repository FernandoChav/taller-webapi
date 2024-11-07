using Bogus;
using Microsoft.EntityFrameworkCore;
using Taller1.Model;
using Taller1.Util;

namespace Taller1.Data.Seeder;

public class UserDataSeeder : IDataSeeder<User>
{

    private const int QuantityUsers = 50;
    private readonly Random _random = new Random();
    
    private readonly DbSet<User> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IEncryptStrategy _strategy;

    public UserDataSeeder(ApplicationDbContext dbContext,
        IEncryptStrategy strategy)
    {
        _dbSet = dbContext.Users;
        _strategy = strategy;
        _applicationDbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbSet.Any())
        {
            return;
        }

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
    
    public DbSet<User> Get()
    { 
        return _dbSet;
    }
}