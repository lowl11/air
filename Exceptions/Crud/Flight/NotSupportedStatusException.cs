using Exceptions.Base;

namespace Exceptions.Crud.Flight;

public class NotSupportedStatusException : BaseException
{

    public NotSupportedStatusException(int value)
        : base("Not supported flight status")
    {
        Add("status", value);
    }

}