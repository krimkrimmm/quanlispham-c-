using Microsoft.AspNetCore.Mvc;

namespace TMDT.Models;

public class PaymentInformation
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? CreditCardNumber { get; set; }
    public string? ExpiryDate { get; set; }
    public string? CVV { get; set; }
    public string? BillingAddress { get; set; }
    public float decrease { get; set; }
    public string? VoucherCode { get; set; }
    public Boolean DiscountApplied { get; set; }
    public List<Cart>? carts { get; set; }
    // Các thông tin thanh toán khác...
}
