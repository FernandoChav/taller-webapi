using System.Collections;

namespace Taller1.Validation;

public class RutSequence : IEnumerator<int>
{

    public int Current { get; set; } = 0;
    object? IEnumerator.Current => _sequence[Current];
    
    private readonly IList<int> _sequence = new List<int>()
    {
        2, 3, 4, 5, 6, 7
    };
    
    public bool MoveNext()
    {
        if ((_sequence.Count - 1) == Current)
        {
            Reset();
        }
        
        Current++;
        return true;
    }

    public void Reset()
    {
        Current = 0;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}