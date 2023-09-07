using Aircraft.Data.Dtos.Flight;
using Aircraft.Data.Interfaces.Repositories;
using Aircraft.Data.Interfaces.Services;

namespace Aircraft.Services;

public class FlightService : IFlightService
{

    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<List<FlightDto>> Get(string? origin, string? destination)
    {
        return (await _flightRepository.GetByOriginAndDestination(origin, destination)).
            Select(FlightDto.FromEntity).ToList();
    }

    public async Task<FlightDto> GetById(int id)
        => FlightDto.FromEntity(await _flightRepository.GetById(id));

    public async Task<int> Add(FlightAddDto dto)
    {
        var user = await _flightRepository.Add(dto.ToEntity());
        return user.Id;
    }

    public async Task UpdateStatus(int id, FlightUpdateStatusDto dto)
    {
        var flight = await GetById(id);
        flight.Status = dto.Status;
        await _flightRepository.Update(flight.ToEntity());
    }
    
}