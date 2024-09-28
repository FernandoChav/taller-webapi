using System.ComponentModel.DataAnnotations;

namespace Taller1.Model;

/// <summary>
/// This class is a model that represent a Role
/// <example>
/// Can exists Administrator or User Role
/// </example>
/// </summary>

public class Role
{
    /// <value> This attribute is a auto incremental Integer Identifier</value>
    public int Id { get; set; } = 0;


    /// <value> This attribute represent role name</value>
    [MaxLength(16)]
    public string Name { get; set; } = string.Empty;

}