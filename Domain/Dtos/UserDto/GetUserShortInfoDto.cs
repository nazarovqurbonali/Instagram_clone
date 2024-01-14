namespace Domain.Dtos.UserDto;

public class GetUserShortInfoDto
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserPhoto { get; set; } = null!;
    public string Fullname { get; set; } = null!;
}