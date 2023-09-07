using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Storage.Data.Entities;

namespace Auth.Data.Entities;

public class UserEntity : Entity
{
    
    [Column("username")]
    [Required]
    [MinLength(3), MaxLength(255)]
    public string Username { get; set; }
    
    [Column("password")]
    [Required]
    [MinLength(3), MaxLength(255)]
    public string Passowrd { get; set; }
    
    [Column("role_id")]
    [Required]
    public int RoleId { get; set; }
    
    public RoleEntity Role { get; set; }
    
}