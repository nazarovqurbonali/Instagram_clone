namespace Domain.Filters.UserFilter;

public class UserLogFilter : PaginationFilter
{
    public int? UserId { get; set; }
}