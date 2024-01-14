using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.LocationDto;

public class UpdateLocationDto : LocationDto
{
    [Required]
    public int LocationId { get; set; }
}