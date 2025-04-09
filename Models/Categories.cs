using Microsoft.AspNetCore.Mvc;
namespace TMDT.Models;
public class Categories
{
    public int Id {get; set; }
    public string? Name {get; set; }
    public List<Products>? Products {  get; set; }
}