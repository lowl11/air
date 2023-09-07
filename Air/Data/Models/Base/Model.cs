using Storage.Data.Dtos;

namespace Air.Data.Models.Base;

public abstract class Model
{
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    protected Model(Dto dto)
    {
        Id = dto.Id;
    }
    
}