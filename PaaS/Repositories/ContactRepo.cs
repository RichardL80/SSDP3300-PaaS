using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.ViewModels;
using PaaS.Models;

namespace PaaS.Repositories
{
    public class ContactRepo
    {
        private readonly ApplicationDbContext _db;

        public ContactRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddContactInfo(int userId)
        {
            ContactInfo contactInfo = new ContactInfo
            {
                Phone = "",
                Address1 = "",
                CityId = -1,
                ProvinceId = -1,
                UserId = userId

            };
            _db.ContactInfo.Add(contactInfo);
            _db.SaveChanges();
        }

    }
}