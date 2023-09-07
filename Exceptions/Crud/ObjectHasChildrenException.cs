using Exceptions.Base;

namespace Exceptions.Crud;

public class ObjectHasChildrenException : BaseException
{

    public ObjectHasChildrenException(int id, Type? entityType = null)
        : base($"{(entityType is not null ? entityType.ToString() : "Object")} with ID {id} has children")
    {
    }
    
}