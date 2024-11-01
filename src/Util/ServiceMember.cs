using System.Diagnostics.CodeAnalysis;

namespace Taller1.Util;

/// <summary>
/// This is a class that relates a Type about her implementation type
/// </summary>

public class ServiceMember
{
    
    /// <summary>
    /// This is a type that represent the parent class
    /// </summary>
    public required Type ServiceType { get; set; }

    
    /// <summary>
    /// Thi is a type that represent the implementation class 
    /// </summary>
    
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public required Type ImplementationType { get; set; }

    /// <summary>
    /// Method for create a new instance ServiceMember
    /// </summary>
    /// <typeparam name="TService">A type class that represent parent class</typeparam>
    /// <typeparam name="TImplementation">A type class that represent implementation class</typeparam>
    /// <returns>A new instance from ServiceMember</returns>
    
    public static ServiceMember NewInstance<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes
            .PublicConstructors)]
        TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        return new ServiceMember
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TImplementation)
        };
    }
}