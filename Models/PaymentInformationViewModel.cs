using TMDT.Areas.Identity.Data;

namespace TMDT.Models
{
    public class PaymentInformationViewModel
    {
        public int Id { get; set; }
        public List<Cart>? CartItems { get; set; }
        public float TotalPrice { get; set; }
        public IEnumerable<Voucher> Vouchers { get; set; }
        public string? PaymentMethod { get; set; }
        public string? QRCode { get; set; }
        public string? OrderNumber { get; set; }
    }

}
