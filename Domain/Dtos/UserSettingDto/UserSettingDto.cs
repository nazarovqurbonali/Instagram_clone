using Domain.Enums;

namespace Domain.Dtos.UserSettingDto;

public class UserSettingDto
{
    public int UserId { get; set; }
    public Active NotificationsNewsletter { get; set; }
    public Active NotificationsFollowers { get; set; }
    public Active NotificationsComments { get; set; }
    public Active NotificationsMessages { get; set; }
}