namespace Taller1.Validation;

public class RutHelper
{
    
    private RutHelper() {}

    public static char CalculateLastDigit(string numbers)
    {
        var numberAsStringReversed = numbers.Reverse();
        var sequence = new RutSequence();
        var sum = 0;
        
        foreach(var c in numberAsStringReversed)
        {
            var number = c - '0';
            var numberSequence = sequence.Current;

            sum += number * numberSequence;
            sequence.MoveNext();
        }

        decimal result = sum / 11;
        result = Math.Floor(result) * 11;
        result = sum - result;

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