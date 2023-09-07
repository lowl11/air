using Storage.Data.Dtos;

namespace Storage.Data.Entities;

public abstract class Entity
{

    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Column("updated_at")]
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    protected Entity()
    {
    }
    
    protected Entity(Dto dto)
    {
        Id = dto.Id;
        CreatedAt = dto.CreatedAt;
        UpdatedAt = dto.UpdatedAt;
    }
    
}