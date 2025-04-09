namespace TMDT.Models
{
	public class Whitelist
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string? Name { get; set; }
		public int Number {  get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? ImagePath { get; set; }
		public DateTime? TimeOrder { get; set; }
		public float TotalPrice { get; set; }
	}
}
