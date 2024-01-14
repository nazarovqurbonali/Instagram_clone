namespace Domain.Filters.PostFilter;

public class PostFilter : PaginationFilter
{
    public string? UserId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}