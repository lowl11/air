using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Storage.Data.Entities;

namespace Auth.Data.Entities;

public class RoleEntity : Entity
{
    
    [Column("code")]
    [Required]
    [MinLength(3), MaxLength(255)]
    public string Code { get; set; }
    
}