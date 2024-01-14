namespace Domain.Filters.PostFilter;

public class PostFollowingFilter : PaginationFilter
{
    public string? UserId { get; set; }
}