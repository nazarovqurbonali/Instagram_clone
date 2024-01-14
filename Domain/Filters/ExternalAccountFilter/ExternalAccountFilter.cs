using System.Reflection.PortableExecutable;

namespace Domain.Filters.ExternalAccountFilter;

public class ExternalAccountFilter : PaginationFilter 
{
    public string? AccountName { get; set; }
}