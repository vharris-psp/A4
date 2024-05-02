using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SQLitePCL;

namespace A4.Models
{
    public class PO
    {
    public string Id { get; set; } = "";
    [Required(ErrorMessage = "User Required")]
    public string? UserId { get; set; }
    [Required(ErrorMessage ="Product Required")]
    public string ProductId { get; set; } = "INVALID PRODUCT ID";
    public decimal UnitPrice { get; set; } = decimal.Zero;
    public decimal Total { get; set; } = 0;

    [Required(ErrorMessage ="Quantity Required")]
    public int Quantity { get; set; } = 0;

    
    public User? User { get; set; }
    public Product? Product { get; set; }
    
    }
}

    
