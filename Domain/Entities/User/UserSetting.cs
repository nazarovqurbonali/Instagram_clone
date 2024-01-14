using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities.User;

public class UserSetting
{
    [Key]
    public string UserId { get; set; }
    public User User { get; set; }
    public Active NotificationsNewsletter { get; set; }
    public Active NotificationsFollowers { get; set; }
    public Active NotificationsComments { get; set; }
    public Active NotificationsMessages { get; set; }
}