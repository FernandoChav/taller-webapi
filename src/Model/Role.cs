using System.ComponentModel.DataAnnotations;

namespace Taller1.Model;

public class Role
{

    public int Id { get; set; } = 0;
    
    [MaxLength(16)]
    public string Name { get; set; } = string.Empty;

}