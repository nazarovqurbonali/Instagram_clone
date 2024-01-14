namespace Domain.Filters.UserFilter;

public class UserFilter : PaginationFilter
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
}