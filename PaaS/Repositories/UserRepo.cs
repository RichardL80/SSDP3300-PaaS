using PaaS.Data;
using PaaS.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Models;
using PaaS.Repositories;

public class UserRepo
{
    private readonly ApplicationDbContext _db;
    private readonly ContactRepo _contactRepo;

    public UserRepo(ApplicationDbContext db, ContactRepo contactRepo)
    {
        _db = db;
        _contactRepo = contactRepo;
    }
    public IEnumerable<UserVM> GetAllUsers()
    {
        IEnumerable<UserVM> users = _db.User.Select(u =>
        new UserVM
        {
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).ToList();

        return users;
    }

    public User GetUser(string email)
    {
        User? user = _db.User.FirstOrDefault(u => u.Email == email);
        return user ?? new User();
    }

    public User GetById(int id)
    {
        User? user = _db.User.FirstOrDefault(u => u.UserId == id);
        return user ?? new User();
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

        User newUser = new User
        {
            FirstName = firstName,
            LastName = LastName,
            Email = email,
            Password = password,
            IsVerified = false, // TODO: Implement email verification
            Phone = "", // Changed from the ERD, needed for non-nullable
            RoleId = 3 // All new users are customers by default
        };
        _db.User.Add(newUser);
        _db.SaveChanges();
        int userId = newUser.UserId;
        _ = _contactRepo.AddContactInfo(userId);

    }

    // There is a potential issue here with updating the user's email, but not the AspNetUser's email
    public void UpdateUserContactInfo(UserVM userVM)
    {
        User user = GetById(userVM.UserId);
        user.FirstName = userVM.FirstName;
        user.LastName = userVM.LastName;
        user.Email = userVM.Email;
        user.Phone = userVM.Phone;
        _db.SaveChanges();
    }
}