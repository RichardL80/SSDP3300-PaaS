using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.ViewModels;
using PaaS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PaaS.Controllers;

public class ContactController : Controller
{

    private readonly ContactRepo _contactRepo;
    private readonly UserRepo _userRepo;
    private readonly LocationRepo _locationRepo;
    private readonly OrderRepo _orderRepo;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;


    public ContactController(ContactRepo contactRepo, UserRepo userRepo, LocationRepo locationRepo, OrderRepo orderRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _contactRepo = contactRepo;
        _userRepo = userRepo;
        _locationRepo = locationRepo;
        _orderRepo = orderRepo;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Authorize]
    public IActionResult MyAccount(string userEmail = null)
    {
        userEmail ??= User.Identity?.Name; // If userEmail isn't specified, use the logged in user's email

        User user = _userRepo.GetUser(userEmail);
        IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(user.UserId); // Get contact info for the user
        foreach (var contact in contactInfoVM)
        {
            // Get the city and province names for each contact
            contact.CityName = _locationRepo.GetCityName(contact.CityId);
            contact.ProvinceName = _locationRepo.GetProvinceName(contact.ProvinceId);
        }

        // Using a composite view model to pass both contact info and orders to the view
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
        // Populate the select lists for the city and province
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
    public async Task<IActionResult> EditDetails(UserVM userVM, string oldEmail)
    {
        if (ModelState.IsValid)
        {
            _userRepo.UpdateUserContactInfo(userVM);

            // Update the AspNetUser email
            var user = await _userManager.FindByEmailAsync(oldEmail);
            if (user != null)
            {
                user.UserName = userVM.Email;
                user.Email = userVM.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Update the user's claims
                    var claims = await _userManager.GetClaimsAsync(user);
                    var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (nameClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, nameClaim);
                    }
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, userVM.Email));

                    // Re-authenticate the user to refresh the claims
                    await _signInManager.RefreshSignInAsync(user);

                    return RedirectToAction(nameof(MyAccount), new { userVM.Email });
                }
                else
                {
                    // Handle update failure
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(userVM);
    }

    // Admin search for user details
    public IActionResult AdminUserDetails(string userEmail)
    {
        return RedirectToAction(nameof(MyAccount), new { userEmail });
    }

    [HttpGet]
    public IActionResult ChangePassword(int userId)
    {
        User user = _userRepo.GetById(userId);
        ChangePasswordVM changePasswordVM = new ChangePasswordVM
        {
            UserId = user.UserId,
            Email = user.Email,
            CurrentPassword = user.Password
        };
        return View(changePasswordVM);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Handle user not found
            ModelState.AddModelError(string.Empty, "User not found.");
            return View(model);
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (result.Succeeded)
        {
            // Re-authenticate the user to refresh the claims
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction(nameof(MyAccount));
        }

        // Handle errors
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

}