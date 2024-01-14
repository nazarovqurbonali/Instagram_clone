namespace Domain.Dtos.ViewerDtos;

public class ViewerDto
{
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int? ViewCount { get; set; }
    public int? ViewLike { get; set; }
}