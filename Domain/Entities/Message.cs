using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    public string UserId { get; set; }
    public User.User User { get; set; }
    public string MessageText { get; set; }
    public DateTime SendMassageDate { get; set; }
}