namespace TMDT.Models
{
    public class Donhang
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Tennguoinhan { get; set; }
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public List<ProductOrders> ProductOrders { get; set; }
        public DateTime? Createtime { get; set; }
        public float price { get; set; }
        public string? TransactionId { get; set; }
        public string? Trangthaidonhang { get; set; }
        public string? QRcode { get; set; }
        public string? Error { get; set; }
    }
}
