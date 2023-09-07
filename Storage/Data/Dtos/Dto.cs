using Storage.Data.Entities;

namespace Storage.Data.Dtos;

public abstract class Dto
{

    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    protected Dto()
    {
    }

    protected Dto(Entity entity)
    {
        Id = entity.Id;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt;
    }

}