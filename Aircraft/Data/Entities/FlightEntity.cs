using Aircraft.Data.Enums;
using Storage.Data.Entities;

namespace Aircraft.Data.Entities;

public class FlightEntity : Entity
{
 
    [Column("origin")]
    [Required]
    [MinLength(3), MaxLength(255)]
    public string Origin { get; set; }
    
    [Column("destination")]
    [Required]
    [MinLength(3), MaxLength(255)]
    public string Destination { get; set; }
    
    [Column("departure")]
    [Required]
    public DateTimeOffset Departure { get; set; }
    
    [Column("departure_offset")]
    [Required]
    public TimeSpan DepartureOffset { get; set; }
    
    [Column("arrival")]
    [Required]
    public DateTimeOffset Arrival { get; set; }
    
    [Column("arrival_offset")]
    [Required]
    public TimeSpan ArrivalOffset { get; set; }

    [Column("status")]
    [Required]
    public FlightStatus Status { get; set; }

}