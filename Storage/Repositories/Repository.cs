using Exceptions.Crud;
using Microsoft.EntityFrameworkCore;
using Storage.Data.Entities;

namespace Storage.Repositories;

public abstract class Repository<T>
    where T : Entity
{
    
    private const int DefaultPageSize = 10;
    private readonly DbContext _context;
    private readonly Type _inheritType;
    
    protected int _pageSize;

    protected Repository(DbContext context, Type inheritType)
    {
        _context = context;
        _inheritType = inheritType;
        _pageSize = DefaultPageSize;
        
        // отключение отслеживания зависимостей для апдейтов
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    protected Repository(DbContext context, int pageSize, Type inheritType)
    {
        _context = context;
        _pageSize = pageSize;
        _inheritType = inheritType;
        
        // отключение отслеживания зависимостей для апдейтов
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected virtual IQueryable<T> All()
        => _context.Set<T>();

    protected (int, int) Page(int page)
    {
        if (page <= 0) page = 1;
        page--;
        
        var from = page * _pageSize;
        var to = from + _pageSize;
        
        return (from, to);
    }

    protected async Task<T> Create(Entity entity)
    {
        var result = await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return (T)result.Entity;
    }
    
    protected async Task Change(Entity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _context.ChangeTracker.Clear();
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    protected async Task ExecuteSql(string sqlScript)
        => await _context.Database.ExecuteSqlRawAsync(sqlScript);
    
    protected async Task Remove(Entity entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    protected async Task Save()
        => await _context.SaveChangesAsync();

    protected void ThrowNotFound(Entity entity)
    {
        if (entity == null)
        {
            throw new NotFoundException(_inheritType);
        }
    }
    
    /* setters */
    
    protected void SetPageSize(int pageSize)
        => _pageSize = pageSize;
    
}