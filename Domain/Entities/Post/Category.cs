using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class Category
{
    public int CategoryId { get; set; }
    [MaxLength(45)]
    public string CategoryName { get; set; }
    public List<PostCategory> PostCategories { get; set; }
}