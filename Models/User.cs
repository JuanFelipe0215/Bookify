using System.ComponentModel.DataAnnotations;

namespace Bookify.Models;

public class User
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El Nombre es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Entre 3 y 50 caracteres")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "El Apellido es obligatorio")]
    [StringLength(50, MinimumLength = 3,ErrorMessage = "Entre 3 y 50 caracteres")]
    public string Lastname { get; set; }
    
    [Required(ErrorMessage = "Email es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un email valido")]
    public string Email { get; set; }
    
    [Phone (ErrorMessage = "Ingrese un telefono valido")]
    public string PhoneNumber { get; set; }

    public DateTime DateRegistration { get; set; } = DateTime.Now;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}