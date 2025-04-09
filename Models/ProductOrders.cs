using Microsoft.AspNetCore.Mvc;

namespace TMDT.Models;
public class ProductOrders
{
    public int Id {get; set; }
    public string? Name {get; set; }
    public string? Description {get; set; }
    public string? ImagePath { get; set; }
    public float Price { get; set; }
    public int Number {get; set; }
    public float decrease { get; set; }
    public int CategoryId { get; set; }    
    public string? Size { get; set; }
    public string? Color {  get; set; } 
    public List<ProductReviews>? ProductReviews { get; set; }
    public List<Donhang>? Donhang { get; set; }
    public List<PaymentResult>? PaymentResult { get; set; }
}