using Exceptions.Base;

namespace Exceptions.Crud.Product;

public class CategoryShouldBeChildException : BaseException
{

    public CategoryShouldBeChildException()
        : base("Category should be child")
    {
    }
    
}