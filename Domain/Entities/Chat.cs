namespace Domain.Entities;
public class Chat
{
    public int ChatId { get; set; }
    public string SendUserId { get; set; }
    public User.User SendUser { get; set; }
    public string ReceiveUserId { get; set; }
    public User.User ReceiveUser { get; set; }
    public List<Message> Messages { get; set; }
}