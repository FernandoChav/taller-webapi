using System.Text;

namespace Taller1.Util;

public class Ruts
{

    private static readonly Random Random = new Random();
    
    private Ruts()
    {
    }

    public static string Create(int length)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            builder.Append(
                Random.NextInt64(0, 9)
            );
        }

        var numbersAsString = builder.ToString();
        
        var lastDigit = CalculateLastDigit(numbersAsString);
        return numbersAsString + lastDigit;
    }
    
    /// <summary>
    /// Calculate last digit rut, a rut is a set numbers for identify a person
    /// country chilean
    /// The way for calculate this last rut is using the "Modulo 11", here a
    /// explain: https://validarutchile.cl/como-calcular-el-digito-verificador-del-rut-de-forma-manual-utilizando-el-algoritmo-del-modulo-11/
    /// </summary>
    /// <param name="numbers">A rut as string</param>
    /// <returns>The last digit</returns>
    
    public static char CalculateLastDigit(string numbers)
    {
        var numberAsStringReversed = numbers.Reverse();
        
        var sequence = new RutSequence();
        var sum = 0;
        
        foreach(var c in numberAsStringReversed)
        {
            var number = c - '0';
            var numberSequence = sequence.Get();
            
            sum += number * numberSequence;
            sequence.MoveNext();
        }
        
        decimal result = sum / 11;
        result = Math.Floor(result) * 11;
        result = Math.Abs(sum - result);
        result = 11 - result;
        
        Console.WriteLine("Result = " + result);

        if (result == 10)
        {
            return 'K';
        }

        if (result == 11)
        {
            return '0';
        }

        return (char)result;
    }

    
}