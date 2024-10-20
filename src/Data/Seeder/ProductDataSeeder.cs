using Bogus;
using Microsoft.EntityFrameworkCore;
using Taller1.src.Models;

namespace Taller1.Data.Seeder;

public class ProductDataSeeder : IDataSeeder<Product>
{

    private const int QuantityProducts = 50;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Product> _products;
    private readonly Random _random = new Random();

    public ProductDataSeeder(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _products = applicationDbContext.Products;
    }

    public void Seed()
    {
        if (_products.Any())
        {
            return;
        }

        var idIncremental = 0;

        var productFaker = new Faker<Product>()
            .RuleFor(u => u.Name, f => f.Commerce.ProductName())
            .RuleFor(u => u.Price, _random.Next(2000, 20000))
            .RuleFor(u => u.Stock, _random.Next(0, 100))
            .RuleFor(u => u.ProductType, ProductType.Book)
            .RuleFor(u => u.AbsoluteUri, f => f.Internet.Url())
            .RuleFor(u => u.IdImage, f => "d");

    productFaker.Generate(QuantityProducts)
            .ForEach(product =>
            {
                Console.WriteLine(product.Id);
                _products.Add(product);
            });

        _applicationDbContext.SaveChanges();
        Console.WriteLine("Seeding products");
    }

    public DbSet<Product> Get()
    {
        return _products;
    }
    
}