using Bogus;
using Microsoft.EntityFrameworkCore;
using Taller1.src.Models;

namespace Taller1.Data.Seeder;

/// <summary>
/// Seeder class that generates and populates the Products table with fake data for testing purposes.
/// It uses the Bogus library to generate random product data.
/// </summary>
public class ProductDataSeeder : IDataSeeder<Product>
{
    private const int QuantityProducts = 50;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<Product> _products;
    private readonly Random _random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductDataSeeder"/> class.
    /// </summary>
    /// <param name="applicationDbContext">The instance of the application database context to interact with the database.</param>
    public ProductDataSeeder(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _products = applicationDbContext.Products;
    }

    /// <summary>
    /// Seeds the Products table with fake data if it is empty.
    /// Uses the Bogus library to generate random product names, prices, stock levels, product types, and URLs.
    /// </summary>
    public void Seed()
    {
        if (_products.Any())
        {
            return;
        }

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
    }

    /// <summary>
    /// Retrieves the DbSet of Products from the database.
    /// </summary>
    /// <returns>The DbSet representing the Products table.</returns>
    public DbSet<Product> Get()
    {
        return _products;
    }
}