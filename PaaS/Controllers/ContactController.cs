using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.ViewModels;
using PaaS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis;

namespace PaaS.Controllers;

public class ContactController : Controller
{

    private readonly ContactRepo _contactRepo;
    private readonly UserRepo _userRepo;
    private readonly LocationRepo _locationRepo;

    public ContactController(ContactRepo contactRepo, UserRepo userRepo, LocationRepo locationRepo)
    {
        _contactRepo = contactRepo;
        _userRepo = userRepo;
        _locationRepo = locationRepo;
    }

    [Authorize]
    public IActionResult MyAccount()
    {
        User user = _userRepo.GetUser(User.Identity.Name);
        IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(user.UserId);
        foreach (var contact in contactInfoVM)
        {
            contact.CityName = _locationRepo.GetCityName(contact.CityId);
            contact.ProvinceName = _locationRepo.GetProvinceName(contact.ProvinceId);
        }
        return View(contactInfoVM);
    }

    [HttpGet]
    public IActionResult AddAddress(int userId)
    {
        ContactInfo contactInfo = _contactRepo.AddContactInfo(userId);

        return RedirectToAction(nameof(EditAddress), new { contactId = contactInfo.ContactId });
    }

    [HttpGet]
    public IActionResult EditAddress(int contactId)
    {
        ContactInfoVM contactInfo = _contactRepo.GetContactInfoById(contactId);
        ViewBag.CitySelectList = _locationRepo.GetCitySelectList();
        ViewBag.ProvinceSelectList = _locationRepo.GetProvinceSelectList();
        return View(contactInfo);
    }

    [HttpPost]
    public IActionResult EditAddress(ContactInfoVM contactInfo)
    {
        if (ModelState.IsValid)
        {
            _contactRepo.UpdateContactInfo(contactInfo);
            return RedirectToAction(nameof(MyAccount));
        }
        return View();
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
    public IActionResult EditDetails(UserVM userVM)
    {

        if (ModelState.IsValid)
        {

            _userRepo.UpdateUserContactInfo(userVM);
            return RedirectToAction(nameof(MyAccount));
        }
        return View();
    }



}