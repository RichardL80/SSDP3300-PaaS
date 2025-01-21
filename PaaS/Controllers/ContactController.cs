using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.ViewModels;
using PaaS.Repositories;

namespace PaaS.Controllers;

public class ContactController : Controller
{

    // private readonly ContactRepo _contactRepo;
    private readonly UserRepo? _userRepo;

    public IActionResult MyAccount()
    {
        UserVM user = _userRepo!.GetUser(User.Identity!.Name);
        return View(user);
    }
}