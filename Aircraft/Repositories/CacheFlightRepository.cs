using System.Linq.Expressions;
using Aircraft.Data.Entities;
using Aircraft.Data.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Aircraft.Repositories;

public class CacheFlightRepository : IFlightRepository
{

    private const string Key = "get_{0}_{1}";
    
    private readonly FlightRepository _decorator;
    private readonly IMemoryCache _cache;

    public CacheFlightRepository(FlightRepository decorator, IMemoryCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<List<FlightEntity>> GetByOriginAndDestination(string? origin, string? destination)
    {
        return (await _cache.GetOrCreateAsync(
            GetKey(origin, destination),
            async _ 
                => await _decorator.GetByOriginAndDestination(origin, destination))
        )!;
    }

    public async Task<int> Count(Expression<Func<FlightEntity, bool>> predicate)
        => await _decorator.Count(predicate);

    public async Task<float> Average(Expression<Func<FlightEntity, bool>> predicate,
        Expression<Func<FlightEntity, float>> selector)
        => await _decorator.Average(predicate, selector);

    public async Task<List<FlightEntity>> GetAll()
        => await _decorator.GetAll();

    public async Task<(List<FlightEntity>, int, int)> GetPage(int page,
        Func<IQueryable<FlightEntity>, IQueryable<FlightEntity>>? filter = null)
        => await _decorator.GetPage(page, filter);

    public async Task<List<FlightEntity>> GetByIds(List<int> ids)
        => await _decorator.GetByIds(ids);

    public async Task<FlightEntity> GetById(int id)
        => await _decorator.GetById(id);

    public async Task<FlightEntity> Add(FlightEntity entity)
    {
        var flight = await _decorator.Add(entity);
        await Task.Run(async () =>
        {
            await RefreshGetByOriginAndDestination(entity.Origin, entity.Destination);
            await RefreshGetByOriginAndDestination(null, null);
            await RefreshGetByOriginAndDestination(entity.Origin, null);
            await RefreshGetByOriginAndDestination(null, entity.Destination);
        });
        return flight;
    }

    public async Task Update(FlightEntity entity)
    {
        await _decorator.Update(entity);
        await Task.Run(async () =>
        {
            await RefreshGetByOriginAndDestination(entity.Origin, entity.Destination);
            await RefreshGetByOriginAndDestination(null, null);
            await RefreshGetByOriginAndDestination(entity.Origin, null);
            await RefreshGetByOriginAndDestination(null, entity.Destination);
        });
    }

    public async Task Delete(FlightEntity entity)
        => await _decorator.Delete(entity);

    public async Task Delete(int id)
        => await _decorator.Delete(id);

    private static string GetKey(string? origin, string? destination)
        => string.Format(Key, origin, destination);

    private async Task RefreshGetByOriginAndDestination(string? origin, string? destination)
    {
        var both = await _decorator.GetByOriginAndDestination(origin, destination);
        _cache.Set(GetKey(origin, destination), both);
    }

}