using Aircraft.Data.Entities;
using Aircraft.Data.Enums;

namespace Aircraft.Data.Dtos.Flight;

public class FlightAddDto
{
    
    public string Origin { get; set; }
    
    public string Destination { get; set; }
    
    public DateTimeOffset Departure { get; set; }
    
    public DateTimeOffset Arrival { get; set; }

    public FlightEntity ToEntity()
    {
        return new FlightEntity()
        {
            Origin = Origin,
            Destination = Destination,
            Departure = Departure,
            DepartureOffset = Departure.Offset,
            Arrival = Arrival,
            ArrivalOffset = Arrival.Offset,
            Status = FlightStatus.InTime,
        };
    }
    
}