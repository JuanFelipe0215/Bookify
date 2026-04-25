using Bookify.Models;
using Bookify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers;

public class LoanController : Controller
{
    private readonly LoanServices _loanServices;
    private readonly UserServices _userServices;
    private readonly BookServices _bookServices;

    public LoanController(LoanServices loanServices, UserServices userServices, BookServices bookServices)
    {
        _loanServices = loanServices;
        _userServices = userServices;
        _bookServices = bookServices;
    }

    public IActionResult Index()
    {
        var loans = _loanServices.GetAllLoans();
        return View(loans.Data);
    }

    public IActionResult Create()
    {
        ViewBag.Users = _userServices.GetAllUsers().Data;
        ViewBag.Books = _bookServices.GetAllBooks().Data;
        return View();
    }

    [HttpPost]
    public IActionResult Store(Loan loan)
    {
        var result = _loanServices.CreateLoan(loan);

        TempData["message"] = result.Message;

        if (result.Success)
            return RedirectToAction("Index");

        ViewBag.Users = _userServices.GetAllUsers().Data;
        ViewBag.Books = _bookServices.GetAllBooks().Data;
        return RedirectToAction("Create");
    }

    public IActionResult Edit(int id)
    {
        var result = _loanServices.EditLoan(id);

        if (!result.Success)
        {
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        return View(result.Data);
    }

    [HttpPost]
    public IActionResult Update(Loan loan)
    {
        var result = _loanServices.UpdateLoan(loan);

        TempData["message"] = result.Message;

        if (result.Success)
            return RedirectToAction("Index");

        return RedirectToAction("Edit", new { id = loan.Id });
    }

    public IActionResult Show(int id)
    {
        var result = _loanServices.GetLoanById(id);

        if (!result.Success)
        {
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        return View(result.Data);
    }

    public IActionResult Delete(int id)
    {
        var result = _loanServices.GetLoanById(id);

        if (!result.Success)
        {
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        return View(result.Data);
    }

    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var result = _loanServices.DeleteLoan(id);

        TempData["message"] = result.Message;
        return RedirectToAction("Index");
    }
}