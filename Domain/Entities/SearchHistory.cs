namespace Domain.Entities;

public class SearchHistory
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User.User User { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}