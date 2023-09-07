using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Storage.Data.Entities;
using Storage.Data.Interfaces;

namespace Storage.Repositories;

public class CrudRepository<T> : Repository<T>, ICrudRepository<T>
    where T : Entity
{
    
    private Expression<Func<T, bool>> _predicate;
    private Func<IQueryable<T>, IQueryable<T>> _apply;

    protected CrudRepository(DbContext context)
        : base(context, typeof(T))
    {
        _predicate = x => true;
        _apply = entities => entities; // default apply
    }

    public async Task<int> Count(Expression<Func<T, bool>> expression)
        => await All().Where(expression).CountAsync();
    
    public async Task<float> Average(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, float>> selector
    ) => await All().Where(predicate).AverageAsync(selector);

    public async Task<List<T>> GetAll() 
        => await List(expression: null);

    public async Task<(List<T>, int, int)> GetPage(
        int page,
        Func<IQueryable<T>, IQueryable<T>>? expression = null
    ) => await ListPage(page, expression);
    
    public async Task<List<T>> GetByIds(List<int> ids)
        => await List(record => ids.Contains(record.Id));

    public async Task<T> GetById(int id)
        => await Single(record => record.Id == id);

    public async Task<T> Add(T entity)
        => await Create(entity);

    public async Task Update(T entity)
        => await Change(entity);

    public async Task Delete(T entity)
        => await Remove(entity);

    public async Task Delete(int id)
    {
        var record = await GetById(id);
        await Remove(record);
    }
    
    /* protected CRUD methods */
    
    protected async Task<List<T>> List(Expression<Func<T, bool>>? expression = null)
    {
        var query = All().Where(_predicate);
        if (expression is not null) query = query.Where(expression);
        return await query.ToListAsync();
    }
    
    protected async Task<List<T>> List(Func<IQueryable<T>, IQueryable<T>>? apply = null)
    {
        var query = All().Where(_predicate);
        if (apply is not null) query = apply(query);
        return await query.ToListAsync();
    }

    protected async Task<(List<T>, int, int)> ListPage(
        int page,
        Func<IQueryable<T>, IQueryable<T>>? expression = null
    )
    {
        var query = All().Where(_predicate);

        if (expression is not null) query = expression(query);
        
        var count = await query.CountAsync();
        var maxPages = count / _pageSize;
        if (count % _pageSize > 0) maxPages++;
        
        var (from, to) = Page(page);
        return (await query.
            Skip(from).Take(to).
            ToListAsync(), count, maxPages);
    }
    
    protected async Task<T> Single(Expression<Func<T, bool>> expression)
    {
        var record = await SingleOrDefault(expression);
        ThrowNotFound(record);
        return record;
    }

    protected async Task<T?> SingleOrDefault(Expression<Func<T, bool>> expression)
        => await All().Where(_predicate).SingleOrDefaultAsync(expression);
    
    /* setters */
    
    protected void SetPredicate(Expression<Func<T, bool>> predicate)
        => _predicate = predicate;

    protected void ApplyQuery(Func<IQueryable<T>, IQueryable<T>> source)
        => _apply = source;

    /* base overrides */
    
    protected override IQueryable<T> All()
    {
        var query = base.All().Where(_predicate);
        return _apply(query);
    }

}