using Domain.Enums;

namespace Domain.Dtos.UserProfileDto;

public class UpdateUserProfileDto : UserProfileDto
{
    public Gender? Gender { get; set; }

}