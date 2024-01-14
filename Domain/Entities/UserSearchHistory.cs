namespace Domain.Entities;

public class UserSearchHistory
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string UserSearchId { get; set; } = null!;
    public User.User UserSearch { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}