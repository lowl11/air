using Air.Data.Models.Base;
using Aircraft.Data.Dtos.Flight;
using Aircraft.Data.Enums;

namespace Air.Data.Models.Flight;

public class FlightModel : Model
{

    [JsonPropertyName("origin")]
    public string Origin { get; set; }
    
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
    
    [JsonPropertyName("departure")]
    public DateTimeOffset Departure { get; set; }
    
    [JsonPropertyName("arrival")]
    public DateTimeOffset Arrival { get; set; }
    
    [JsonPropertyName("status")]
    public FlightStatus Status { get; set; }
    
    public FlightModel(FlightDto dto)
        : base(dto)
    {
        Origin = dto.Origin;
        Destination = dto.Destination;
        Departure = dto.Departure;
        Arrival = dto.Arrival;
        Status = dto.Status;
    }

    public static FlightModel FromDto(FlightDto dto) => new(dto);

}