namespace Taller1.Model;

public class ShoppingCart
{
    public Dictionary<int, ProductShoppingCart?> Elements { get; } = [];

    public void Add(ProductShoppingCart productShoppingCart)
    {
        var id = productShoppingCart.GetId();
        var anotherProductShoppingCart = Elements[id];

        if (anotherProductShoppingCart == null)
        {
            Elements[id] = productShoppingCart;
            return;
        }

        anotherProductShoppingCart.
            Increase(productShoppingCart.GetStock());
    }

    public void Remove(int id, int stock)
    {
        var productShoppingCart = Elements[id];
        if (productShoppingCart == null)
        {
            return;
        }
        productShoppingCart.Increase(stock);
    }
    
    public bool Exists(int id)
    {
        return Elements.ContainsKey(id);
    }
}

public class ProductShoppingCart
{
    private readonly int _id;
    private int _stock;

    public ProductShoppingCart(int id,
        int stock)
    {
        this._id = id;
        this._stock = stock;
    }

    public int GetId()
    {
        return _id;
    }

    public int GetStock()
    {
        return _stock;
    }

    public void Increase(int stock)
    {
        _stock += stock;
    }

    public void Decrease(int stock)
    {
        _stock -= stock;

        if (_stock < 0)
        {
            _stock = 0;
        }
    }
}