namespace Domain.Dtos.UserDto;

public class GetUserDto : UserDto
{
    public string Id { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int SubscribersCount { get; set; }
}