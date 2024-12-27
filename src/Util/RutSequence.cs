using System.Collections;

namespace Taller1.Util;

/// <summary>
/// This is implementation of IEnumator, using for get values
/// from "Modulo 11"
/// </summary>

public class RutSequence : IEnumerator<int>
{

    /// <summary>
    /// Actual index value
    /// </summary>
    private int Index { get; set; } = 0;
    
    /// <summary>
    /// Actual value from index
    /// </summary>
    object? IEnumerator.Current => _sequence[Index];
    
    /// <summary>
    /// A list from elements 
    /// </summary>
    
    private readonly IList<int> _sequence = new List<int>()
    {
        2, 3, 4, 5, 6, 7
    };
    
    /// <summary>
    /// Get next element from list
    /// </summary>
    /// <returns>A integer that represent the next value</returns>
    
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

    /// <summary>
    /// Get actual value from the list
    /// </summary>
    /// <returns>The actual value</returns>
    
    public int Get()
    {
        return _sequence[Index];
    }
    
    /// <summary>
    /// Reset the index for get the first index
    /// </summary>

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