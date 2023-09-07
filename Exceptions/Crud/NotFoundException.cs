using Exceptions.Base;

namespace Exceptions.Crud;

public class NotFoundException : BaseException
{
    private string? EntityType { get; set; }
    
    public NotFoundException(Type entityType, string? id = null, string message = "Not Found")
        : base(message)
    {
        EntityType = entityType.ToString();
        Add("type", EntityType);

        if (id != null)
        {
            Add("id", id);
        }
    }
    
}