using Bookify.Models;
using Bookify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers;

public class UserController:Controller
{
    private readonly UserServices _services;

    public UserController(UserServices services)
    {
        _services = services;
    }

    public IActionResult Index()
    {
        var users = _services.GetAllUsers();
        return View(users.Data);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Store(User user)
    {
        var users = _services.CreateUser(user);

        if (users.Success)
        {
            TempData["message"] = users.Message;
            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = users.Message;
            return RedirectToAction("Create");
        }
    }


    public IActionResult Edit(int id)
    {
        var userEdit = _services.EditUser(id);
        return View(userEdit.Data);
    }

    [HttpPost]
    public IActionResult Update(User user)
    {
        var updateUser = _services.UpdateUser(user);

        if (updateUser.Success)
        {
            TempData["message"] = updateUser.Message;
            return RedirectToAction("Index");
        }
        else
        {
            TempData["message"] = updateUser.Message;
            return RedirectToAction("Edit");
        }
    }

    public IActionResult Show(int id)
    {
        var user = _services.ShowUser(id);
        return View(user.Data);

    }

    public IActionResult Delete(int id)
    {
        var user = _services.Delete(id);
        return View(user.Data);
    }
    
    
    [HttpPost]
    public IActionResult Destroy(User user)
    {
        var deleteUser = _services.DeleteUser(user);

        TempData["message"] = deleteUser.Message;

        return RedirectToAction("Index");

    }
    
    


}