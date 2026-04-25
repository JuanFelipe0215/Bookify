using Bookify.Data;
using Bookify.Models;
using Bookify.Response;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Services;

public class LoanServices
{
    private readonly DataContext _context;

    public LoanServices(DataContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<Loan>> GetAllLoans()
    {
        var response = new ServiceResponse<IEnumerable<Loan>>();

        try
        {
            var loans = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .OrderByDescending(l => l.LoanDate)
                .ToList();

            response.Data = loans;
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al obtener los préstamos";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Loan> GetLoanById(int id)
    {
        var response = new ServiceResponse<Loan>();

        try
        {
            var loan = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .FirstOrDefault(l => l.Id == id);

            if (loan == null)
            {
                response.Message = "Préstamo no encontrado";
                response.Success = false;
                return response;
            }

            response.Data = loan;
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al obtener el préstamo";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Loan> CreateLoan(Loan loan)
    {
        var response = new ServiceResponse<Loan>();

        try
        {
            var book = _context.Books.Find(loan.IdBook);

            if (book == null)
            {
                response.Message = "El libro no existe";
                response.Success = false;
                return response;
            }

            if (book.Quantity <= 0)
            {
                response.Message = "No hay ejemplares disponibles de este libro";
                response.Success = false;
                return response;
            }
            
            book.Quantity -= 1;
            _context.Books.Update(book);

            loan.LoanDate = DateTime.Now;
            loan.Status = StatusLoan.Prestado;

            _context.Loans.Add(loan);
            _context.SaveChanges();

            response.Data = loan;
            response.Message = "Préstamo registrado correctamente";
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al registrar el préstamo";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Loan> EditLoan(int id)
    {
        var response = new ServiceResponse<Loan>();

        try
        {
            var loan = _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .FirstOrDefault(l => l.Id == id);

            if (loan == null)
            {
                response.Message = "Préstamo no encontrado";
                response.Success = false;
                return response;
            }

            response.Data = loan;
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al obtener el préstamo";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Loan> UpdateLoan(Loan loan)
    {
        var response = new ServiceResponse<Loan>();

        try
        {
            var existing = _context.Loans.Find(loan.Id);

            if (existing == null)
            {
                response.Message = "Préstamo no encontrado";
                response.Success = false;
                return response;
            }
            
            if (existing.Status != StatusLoan.Devuelto && loan.Status == StatusLoan.Devuelto)
            {
                var book = _context.Books.Find(existing.IdBook);
                if (book != null)
                {
                    book.Quantity += 1;
                    _context.Books.Update(book);
                }
            }

            existing.Status = loan.Status;
            existing.ReturnDate = loan.ReturnDate;

            _context.Loans.Update(existing);
            _context.SaveChanges();

            response.Data = existing;
            response.Message = "Préstamo actualizado correctamente";
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al actualizar el préstamo";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Loan> DeleteLoan(int id)
    {
        var response = new ServiceResponse<Loan>();

        try
        {
            var loan = _context.Loans.Find(id);

            if (loan == null)
            {
                response.Message = "Préstamo no encontrado";
                response.Success = false;
                return response;
            }
            
            if (loan.Status == StatusLoan.Prestado)
            {
                var book = _context.Books.Find(loan.IdBook);
                if (book != null)
                {
                    book.Quantity += 1;
                    _context.Books.Update(book);
                }
            }

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            response.Data = loan;
            response.Message = "Préstamo eliminado correctamente";
            response.Success = true;
        }
        catch (Exception e)
        {
            response.Message = "Error al eliminar el préstamo";
            response.Success = false;
        }

        return response;
    }
}