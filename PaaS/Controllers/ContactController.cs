using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.ViewModels;
using PaaS.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace PaaS.Controllers;

public class ContactController : Controller
{

    private readonly ContactRepo _contactRepo;
    private readonly UserRepo _userRepo;

    public ContactController(ContactRepo contactRepo, UserRepo userRepo)
    {
        _contactRepo = contactRepo;
        _userRepo = userRepo;
    }

    [Authorize]
    public IActionResult MyAccount()
    {
        User user = _userRepo.GetUser(User.Identity.Name);
        IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(user.UserId);
        return View(contactInfoVM);
    }
}