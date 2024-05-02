using System.ComponentModel.DataAnnotations;

namespace A4.Models
{
    public class Product{
    public string? Id { get; set; }
    [Required(ErrorMessage = "Product Name Required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Product Description Required")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Price Required")]
    public decimal Price { get; set; } = 0;
    [Required(ErrorMessage = "Available Quantity Required")]  
    public int? AvailableQuantity { get; set; } = 0;
    }

}

