using System.Diagnostics.CodeAnalysis;

namespace Taller1.Util;

public class ServiceMember
{
    public Type ServiceType { get; set; }

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public Type ImplementationType { get; set; }

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