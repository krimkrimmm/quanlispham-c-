namespace TMDT.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public bool IsUsed { get; set; }
    }

}
