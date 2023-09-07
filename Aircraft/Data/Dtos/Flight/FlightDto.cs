using Aircraft.Data.Entities;
using Aircraft.Data.Enums;
using Storage.Data.Dtos;

namespace Aircraft.Data.Dtos.Flight;

public class FlightDto : Dto
{
    
    public string Origin { get; set; }
    
    public string Destination { get; set; }
    
    public DateTimeOffset Departure { get; set; }
    
    public DateTimeOffset Arrival { get; set; }
    
    public FlightStatus Status { get; set; }

    public FlightDto(FlightEntity entity)
        : base(entity)
    {
        Origin = entity.Origin;
        Destination = entity.Destination;

        Departure = new DateTimeOffset(entity.Departure.DateTime, entity.DepartureOffset);
        Arrival = new DateTimeOffset(entity.Arrival.DateTime, entity.ArrivalOffset);

        Status = entity.Status;
    }

    public static FlightDto FromEntity(FlightEntity entity) => new(entity);

    public FlightEntity ToEntity()
    {
        return new FlightEntity()
        {
            Id = Id,
            Origin = Origin,
            Destination = Destination,
            Departure = Departure,
            Arrival = Arrival,
            Status = Status,
        };
    }

}