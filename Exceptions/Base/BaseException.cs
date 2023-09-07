using System.Collections;

namespace Exceptions.Base;

public class BaseException : Exception
{

    private readonly Hashtable _context;

    protected BaseException(string message) 
        : base(message)
    {
        _context = new Hashtable();
    }

    protected void Add(string key, object value)
    {
        _context.Add(key, value);
    }

    public Hashtable GetContext() => _context;

}