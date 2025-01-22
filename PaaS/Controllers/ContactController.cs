using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.ViewModels;
using PaaS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    [HttpGet]
    public IActionResult EditAddress(int userId)
    {
        IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(userId);
        return View(contactInfoVM);
    }

    [HttpGet]
    public IActionResult EditDetails(int userId)
    {
        User user = _userRepo.GetById(userId);
        UserVM userVM = new UserVM
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone
        };
        return View(userVM);
    }

    [HttpPost]
    public IActionResult EditDetails(int userId, string firstName, string lastName, string email, string phone)
    {

        if (ModelState.IsValid)
        {
            //_contactRepo.UpdateContactInfo(userId, firstName, lastName, email, phone);
            return RedirectToAction(nameof(MyAccount));
        }
        return View();
    }


}