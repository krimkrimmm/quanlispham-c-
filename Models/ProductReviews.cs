using Microsoft.AspNetCore.Mvc;

namespace TMDT.Models;
public class ProductReviews
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? UserId { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public static implicit operator ProductReviews(List<ProductReviews> v)
    {
        throw new NotImplementedException();
    }

    // Các thuộc tính khác nếu cần
}
