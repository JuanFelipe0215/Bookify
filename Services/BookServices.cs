using Bookify.Data;
using Bookify.Models;
using Bookify.Response;

namespace Bookify.Services;

public class BookServices
{
    private readonly DataContext _context;

    public BookServices(DataContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<Book>> GetAllBooks()
    {
        var books = _context.Books.ToList();
        return new ServiceResponse<IEnumerable<Book>>()
        {
            Data = books,
            Success = true
        };
    }


    public ServiceResponse<Book> StoreBook(Book book)
    {
        var response = new ServiceResponse<Book>();

        try
        {

            book.Quantity = book.Stock;
            
            _context.Books.Add(book);
            _context.SaveChanges();
            response.Message = "Libro Creado Correctamente";
            response.Data = book;
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "Error al crear el libro";
            response.Success = false;
        }

        return response;
    }
    
    
    public ServiceResponse<Book> EditBook(int id)
    {
        var response = new ServiceResponse<Book>();

        try
        {
            var book = _context.Books.Find(id);

            response.Message = "Libro encontrado";
            response.Data = book;
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "Libro no encontrado";
            response.Success = false;
        }

        return response;
    }

    public ServiceResponse<Book> UpdateBook(Book book)
    {
        var responseUpdate = new ServiceResponse<Book>();

        try
        {
            _context.Books.Update(book);
            _context.SaveChanges();

            responseUpdate.Data = book;
            responseUpdate.Message = "Libro Actualizado correctamente";
            responseUpdate.Success = true;

        }
        catch (Exception e)
        {
            responseUpdate.Success = false;
            responseUpdate.Message = "No se puedo actualizar el libro";
        }

        return responseUpdate;
    }

    public ServiceResponse<Book> Show(int id)
    {
        var responseShow = new ServiceResponse<Book>();

        try
        {
            var bookShow = _context.Books.Find(id);
            responseShow.Data = bookShow;
            responseShow.Message = "Libro encontrado";
            responseShow.Success = true;

        }
        catch (Exception e)
        {
            responseShow.Message = e.Message;
            responseShow.Success = false;
        }


        return responseShow;
    }

    public ServiceResponse<Book> Delete(int id)
    {
        var book = _context.Books.Find(id);

        return new ServiceResponse<Book>()
        {
            Data = book
        };
    }
    
    
    public ServiceResponse<Book> Destroy(Book book)
    {
        var response = new ServiceResponse<Book>();

        try
        {
            _context.Books.Remove(book);
            _context.SaveChanges();

            response.Data = book;
            response.Message = "Eliminado Correctamente";
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;

    }
    
    
}