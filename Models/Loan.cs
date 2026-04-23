using System.ComponentModel.DataAnnotations;

namespace Bookify.Models;

public class Loan
{
    public int Id { get; set; }

    public DateTime LoanDate { get; set; } = DateTime.Now;
    
    public DateTime? ReturnDate { get; set; }

    public StatusLoan Status { get; set; } = StatusLoan.Prestado;
    
    public int IdUser { get; set; }
    public User User { get; set; }
    
    public int IdBook { get; set; }
    public Book Book { get; set; }
    
}