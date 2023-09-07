using System.ComponentModel.DataAnnotations;
using Aircraft.Data.Dtos.Flight;
using Aircraft.Data.Enums;

namespace Air.Data.Models.Flight;

public class FlightUpdateStatusModel
{
    
    [JsonPropertyName("status")]
    [Required]
    public FlightStatus Status { get; set; }

    public FlightUpdateStatusDto ToDto()
    {
        return new FlightUpdateStatusDto()
        {
            Status = Status,
        };
    }
    
}