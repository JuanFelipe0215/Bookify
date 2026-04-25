using Bookify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers;

public class HomeController : Controller
{
    private readonly UserServices _userServices;
    private readonly BookServices _bookServices;
    private readonly LoanServices _loanServices;

    public HomeController(UserServices userServices, BookServices bookServices, LoanServices loanServices)
    {
        _userServices = userServices;
        _bookServices = bookServices;
        _loanServices = loanServices;
    }

    public IActionResult Index()
    {
        var users = _userServices.GetAllUsers();
        var books = _bookServices.GetAllBooks();
        var loans = _loanServices.GetAllLoans();

        ViewBag.TotalUsers  = users.Data?.Count()  ?? 0;
        ViewBag.TotalBooks  = books.Data?.Count()  ?? 0;
        ViewBag.TotalLoans  = loans.Data?.Count()  ?? 0;
        ViewBag.ActiveLoans = loans.Data?.Count(l => l.Status == Bookify.Models.StatusLoan.Prestado) ?? 0;
        ViewBag.OverdueLoans = loans.Data?.Count(l => l.Status == Bookify.Models.StatusLoan.Vencido) ?? 0;
        ViewBag.RecentLoans = loans.Data?.OrderByDescending(l => l.LoanDate).Take(5).ToList();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}