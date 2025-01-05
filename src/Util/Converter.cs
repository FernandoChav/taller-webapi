using Taller1.src.Models;

namespace Taller1.Util;

public class Converter
{

    private static readonly IDictionary<int, ProductType> IntToProduct = new
    Dictionary<int, ProductType>(){
        {0, ProductType.TShirt},
        {1, ProductType.Cap},
        {2, ProductType.Toy},
        {3, ProductType.Food},
        {4, ProductType.Book}
    };

    public static ProductType ConvertToProductType(int number)
    {

        if (number == null)
        {
            return ProductType.Book;
        }
        
        return IntToProduct[number];
    }

}