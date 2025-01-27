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
    private readonly OrderRepo _orderRepo;

    public ContactController(ContactRepo contactRepo, UserRepo userRepo, LocationRepo locationRepo, OrderRepo orderRepo)
    {
        _contactRepo = contactRepo;
        _userRepo = userRepo;
        _locationRepo = locationRepo;
        _orderRepo = orderRepo;
    }

    [Authorize]
    public IActionResult MyAccount(string userEmail = null)
    {
        userEmail ??= User.Identity?.Name;

        User user = _userRepo.GetUser(userEmail);
        if (user == null)
        {
            // Handle the case where user is not found
            return RedirectToAction("Error", "Home");
        }

        IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(user.UserId);
        foreach (var contact in contactInfoVM)
        {
            contact.CityName = _locationRepo.GetCityName(contact.CityId);
            contact.ProvinceName = _locationRepo.GetProvinceName(contact.ProvinceId);
        }

        var myAccountVM = new MyAccountVM
        {
            ContactInfo = contactInfoVM,
            Orders = _orderRepo.GetOrderByUserId(user.UserId)
        };

        return View(myAccountVM);
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
            return RedirectToAction(nameof(MyAccount), new { contactInfo.Email });
        }
        return View();
    }

    [HttpGet]
    public IActionResult DeleteAddress(int contactId)
    {
        return View(_contactRepo.GetContactInfoById(contactId));
    }

    [HttpPost]
    public IActionResult DeleteAddress(ContactInfoVM contactInfo)
    {
        int contactId = contactInfo.ContactId ?? -1; // Fixing nullable error
        _contactRepo.DeleteContactInfo(contactId);
        return RedirectToAction(nameof(MyAccount), new { contactInfo.Email });
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
            return RedirectToAction(nameof(MyAccount), new { userVM.Email });
        }
        return View();
    }

    public IActionResult AdminUserDetails(string userEmail)
    {
        return RedirectToAction(nameof(MyAccount), new { userEmail });
    }

}