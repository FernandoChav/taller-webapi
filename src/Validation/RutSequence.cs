using System.Collections;

namespace Taller1.Validation;

public class RutSequence : IEnumerator<int>
{

    private int Index { get; set; } = 0;
    object? IEnumerator.Current => _sequence[Index];
    
    private readonly IList<int> _sequence = new List<int>()
    {
        2, 3, 4, 5, 6, 7
    };
    
    public bool MoveNext()
    {
        if ((_sequence.Count - 1) == Index)
        {
            Reset();
            return true;
        }

        Index++;
        return true;
    }

    public int Get()
    {
        return _sequence[Index];
    }

    public void Reset()
    {
        Index = 0;
    }

    public int Current { get; } = 0;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}