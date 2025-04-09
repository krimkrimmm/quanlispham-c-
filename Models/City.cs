namespace TMDT.Models
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Districts>? Districts { get; set; }
    }
}
