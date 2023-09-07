using System.ComponentModel.DataAnnotations;
using Aircraft.Data.Dtos.Flight;
using Aircraft.Data.Enums;
using Exceptions.Crud.Flight;

namespace Air.Data.Models.Flight;

public class FlightUpdateStatusModel
{
    
    [JsonPropertyName("status")]
    [Required]
    public FlightStatus Status { get; set; }

    public FlightUpdateStatusDto ToDto()
    {
        FlightStatus parsed;
        switch (Status)
        {
            case FlightStatus.InTime:
            case FlightStatus.Delayed:
            case FlightStatus.Cancelled:
                parsed = Status;
                break;
            default:
                throw new NotSupportedStatusException((int)Status);
        }
        
        return new FlightUpdateStatusDto()
        {
            Status = parsed,
        };
    }
    
}