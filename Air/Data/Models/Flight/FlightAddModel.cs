using System.ComponentModel.DataAnnotations;
using Aircraft.Data.Dtos.Flight;

namespace Air.Data.Models.Flight;

public class FlightAddModel
{
    
    [JsonPropertyName("origin")]
    [Required]
    public string Origin { get; set; }
    
    [JsonPropertyName("destination")]
    [Required]
    public string Destination { get; set; }
    
    [JsonPropertyName("departure")]
    [Required]
    public DateTimeOffset Departure { get; set; }
    
    [JsonPropertyName("arrival")]
    [Required]
    public DateTimeOffset Arrival { get; set; }

    public FlightAddDto ToDto()
    {
        return new FlightAddDto()
        {
            Origin = Origin,
            Destination = Destination,
            Departure = Departure,
            Arrival = Arrival,
        };
    }
    
}