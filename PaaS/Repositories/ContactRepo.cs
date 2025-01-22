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

        public IEnumerable<ContactInfoVM> GetContactInfo(int userId)
        {
            IEnumerable<ContactInfoVM> contactInfo = _db.ContactInfo.Join(_db.User,
                c => c.UserId,
                u => u.UserId,
                (c, u) => new ContactInfoVM
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    //Phone = c.Phone,
                    Phone = u.Phone,
                    Address1 = c.Address1,
                    Address2 = c.Address2,
                    CityId = c.CityId,
                    ProvinceId = c.ProvinceId,
                    UserId = c.UserId,
                    Orders = "test" //TODO
                }).Where(c => c.UserId == userId).ToList();

            return contactInfo;
        }

        public void AddContactInfo(int userId)
        {
            ContactInfo contactInfo = new ContactInfo
            {
                //Phone = "",
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