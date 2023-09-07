using System.Linq.Expressions;

namespace Storage.Data.Interfaces;

public interface ICrudRepository<T>
{

    Task<int> Count(Expression<Func<T, bool>> predicate);
    Task<float> Average(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, float>> selector
    );
    Task<List<T>> GetAll();
    Task<(List<T>, int, int)> GetPage(
        int page,
        Func<IQueryable<T>, IQueryable<T>>? filter = null
    );
    Task<List<T>> GetByIds(List<int> ids);
    Task<T> GetById(int id);
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task Delete(int id);

}