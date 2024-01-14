namespace Domain.Dtos.ImageDto;

public class ImageDto
{
    public int ImageId { get; set; }
    public int PostId { get; set; }
    public string Path { get; set; } = null!;
}