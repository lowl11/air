using System.Linq.Expressions;
using Aircraft.Data.Contexts;
using Aircraft.Data.Entities;
using Aircraft.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories;

namespace Aircraft.Repositories;

public class FlightRepository : CrudRepository<FlightEntity>, IFlightRepository
{

    public FlightRepository(AircraftContext context)
        : base(context)
    {
        ApplyQuery(flights 
            => flights.OrderBy(flight => flight.Arrival));
    }

    public async Task<List<FlightEntity>> GetByOriginAndDestination(string? origin, string? destination)
    {
        return await List(flights =>
        {
            if (origin is not null)
            {
                flights = flights.Where(flight => flight.Origin == origin);
            }

            if (destination is not null)
            {
                flights = flights.Where(flight => flight.Destination == destination);
            }
            
            return flights;
        });
    }

}