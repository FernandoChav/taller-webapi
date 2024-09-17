namespace Taller1.Mapper;

public interface IObjectMapper<TE1, TE2>
{
    
    TE1 Mapper(TE2 entity);

    TE2 Mapper(TE1 entity);

}