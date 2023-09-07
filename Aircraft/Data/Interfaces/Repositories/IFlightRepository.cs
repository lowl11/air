using Aircraft.Data.Entities;
using Storage.Data.Interfaces;

namespace Aircraft.Data.Interfaces.Repositories;

public interface IFlightRepository : ICrudRepository<FlightEntity>
{

    Task<List<FlightEntity>> GetByOriginAndDestination(string? origin, string? destination);

}