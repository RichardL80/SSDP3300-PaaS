using PaaS.Data;
using PaaS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Models;

public class UserRepo
{
    private readonly ApplicationDbContext _db;
    public UserRepo(ApplicationDbContext db)
    {
        _db = db;
    }
    public IEnumerable<UserVM> GetAllUsers()
    {
        // IEnumerable<UserVM> users = _db.Users.Select(u =>
        // new UserVM { Email = u.Email }).ToList();
        IEnumerable<UserVM> users = _db.User.Select(u =>
        new UserVM
        {
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).ToList();

        return users;
    }
    public SelectList GetUserSelectList(string? email)
    {
        IEnumerable<SelectListItem> users =
        GetAllUsers().Select(u => new SelectListItem
        {
            Value = u.Email,
            Text = u.Email
        });
        SelectList roleSelectList =
        new SelectList(users, "Value", "Text", email);
        return roleSelectList;
    }

    public void AddUser(string firstName, string LastName, string email, string password)
    {
        var newUser = new User
        {
            FirstName = firstName,
            LastName = LastName,
            Email = email,
            Password = password,
            IsVerified = false,
            RoleId = 3
        };
        _db.User.Add(newUser);
        _db.SaveChanges();
    }
}