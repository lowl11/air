using Aircraft.Data.Dtos.Flight;

namespace Aircraft.Data.Interfaces.Services;

public interface IFlightService
{

    Task<List<FlightDto>> Get(string? origin, string? destination);
    Task<int> Add(FlightAddDto dto);
    Task UpdateStatus(int id, FlightUpdateStatusDto dto);

}