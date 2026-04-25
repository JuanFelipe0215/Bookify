using System.ComponentModel.DataAnnotations;

namespace Bookify.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Nombre del libro requerido")]
    public string Title { get; set; }
    
    [Required (ErrorMessage = "Autor requerido")]
    public string Author { get; set; }
    
    [Required(ErrorMessage = "Codigo unico identificador de libro incorrecto")]
    public string Isbn { get; set; }
    
    [Required(ErrorMessage = "Ingrese un tipo de Género")]
    public BookGenre BookGenre { get; set; }
    
    public int? PublicationYear { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
    public int Quantity { get; set; }
    
    public int Stock { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}