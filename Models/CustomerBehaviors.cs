namespace TMDT.Models
{
    public class CustomerBehaviors
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public float TotalPurchases { get; set; }
        public float TotalSpent { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public List<string> FavoriteCategories { get; set; }
    }
}
