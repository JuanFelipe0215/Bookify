using Bookify.Models;
using Bookify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookify.Controllers;

public class BookController:Controller
{
    private readonly BookServices _services;

    public BookController(BookServices services)
    {
        _services = services;
    }

    public IActionResult Index()
    {
        var books = _services.GetAllBooks();
        return View(books.Data);

    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Book book)
    {
        var books = _services.StoreBook(book);

        if (books.Success)
        {
            TempData["message"] = books.Message;
            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = books.Message;
            return RedirectToAction("Create");
        }
        
    }

    public IActionResult Edit(int id)
    {
        var books = _services.EditBook(id);
        return View(books.Data);
    }

    [HttpPost]
    public IActionResult Update(Book book)
    {
        var updateBook = _services.UpdateBook(book);

        if (updateBook.Success)
        {
            TempData["message"] = updateBook.Message;
            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = updateBook.Message;
            return RedirectToAction("Edit");
        }
    }


    public IActionResult Show(int id)
    {
        var book = _services.Show(id);
        return View(book.Data);
    }

    public IActionResult Delete(int id)
    {
        var book = _services.Delete(id);
        return View(book.Data);
    }

    [HttpPost]
    public IActionResult Destroy(Book book)
    {
        var bookDestroy = _services.Destroy(book);

        if (bookDestroy.Success)
        {
            TempData["message"] = bookDestroy.Message;
            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = bookDestroy.Message;
            return RedirectToAction("Delete");
        }
        
    }

}