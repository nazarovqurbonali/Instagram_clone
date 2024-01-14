namespace Domain.Dtos.UserProfileDto;

public class GetUserProfileDto : UserProfileDto
{
    public string UserName { get; set; } = null!;
    public new string Image { get; set; } = null!;
    public DateTime DateUpdated { get; set; }
    public string? Gender { get; set; }
    public int PostCount { get; set; }
    public int SubscribersCount { get; set; }
    public int SubscriptionsCount { get; set; }
}