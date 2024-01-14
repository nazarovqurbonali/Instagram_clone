namespace Domain.Dtos.PostCommentDto;

public class GetPostCommentDto : PostCommentDto
{
    public int PostCommentId { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime DateCommented { get; set; }
}