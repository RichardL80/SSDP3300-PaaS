using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaaS.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace PaaS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // List all users with their roles
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.Where(u => !string.IsNullOrEmpty(u.Email)).ToList(); 
            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                userRoles[user.Email] = await _userManager.GetRolesAsync(user);
            }

            ViewBag.UserRoles = userRoles;
            return View(users);
        }



        [HttpGet("AssignRole")]
        public async Task<IActionResult> AssignRole(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(new UserRoleVM { Email = email });
        }

        // assign role
        [HttpPost("AssignRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(UserRoleVM model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.RoleName))
            {
                return BadRequest("Invalid email or role.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userManager.AddToRoleAsync(user, model.RoleName);

            TempData["SuccessMessage"] = $"✅ Role '{model.RoleName}' assigned to {model.Email} successfully!";

            return RedirectToAction("Index");
        }


        //delete confirm
        [HttpGet]
        public IActionResult ConfirmDeleteUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            return View("ConfirmDeleteUser", new UserRoleVM { Email = email });
        }




        // delete user
        [HttpPost("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = $"✅ User {email} deleted successfully!";

            return RedirectToAction("Index");
        }

    }
}
