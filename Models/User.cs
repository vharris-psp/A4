using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace A4.Models
{
    public class User {
    
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set;} = string.Empty;
    

    public string? Id { get; set; } 
    public string? AccountNumber { get; set; } 

    public string Name { get { return $"{LastName}, {FirstName}"; }}
}


}
