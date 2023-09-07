using Exceptions.Base;

namespace Exceptions.Crud;

public class BindingEntityIsParent : BaseException
{

    public BindingEntityIsParent()
        : base("Entity you trying to bind is parent entity, need child")
    {
    }
    
}