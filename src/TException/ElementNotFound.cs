namespace Taller1.TException;

public class ElementNotFound : Exception
{
    
    public ElementNotFound() : base("Element not found") {}
    
    public ElementNotFound(Exception exception) : base("Element not found", exception) {}
    
}