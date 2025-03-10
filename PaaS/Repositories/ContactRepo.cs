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
                    ContactId = c.ContactId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Password = u.Password,
                    Email = u.Email,
                    //Phone = c.Phone,
                    Phone = u.Phone,
                    Address1 = c.Address1,
                    Address2 = c.Address2,
                    CityId = c.CityId,
                    ProvinceId = c.ProvinceId,
                    UserId = c.UserId,
                }).Where(c => c.UserId == userId).ToList();

            return contactInfo;
        }

        public ContactInfoVM GetContactInfoById(int contactId)
        {
            ContactInfoVM? contactInfoVM = _db.ContactInfo
                .Join(_db.User,
                    c => c.UserId,
                    u => u.UserId,
                    (c, u) => new { c, u })
                .Join(_db.City,
                    cu => cu.c.CityId,
                    city => city.CityId,
                    (cu, city) => new { cu.c, cu.u, city })
                .Join(_db.Province,
                    cuc => cuc.city.ProvinceId,
                    province => province.ProvinceId,
                    (cuc, province) => new ContactInfoVM
                    {
                        ContactId = cuc.c.ContactId,
                        FirstName = cuc.u.FirstName,
                        LastName = cuc.u.LastName,
                        Email = cuc.u.Email,
                        Phone = cuc.u.Phone,
                        Address1 = cuc.c.Address1,
                        Address2 = cuc.c.Address2,
                        CityId = cuc.c.CityId,
                        ProvinceId = cuc.c.ProvinceId,
                        UserId = cuc.c.UserId,
                        CityName = cuc.city.Name,
                        ProvinceName = province.Name
                    })
                .FirstOrDefault(c => c.ContactId == contactId);

            return contactInfoVM ?? new ContactInfoVM();
        }

        public ContactInfo AddContactInfo(int userId)
        {
            // Add a new contact info record, default values because of non-nullable requirements
            ContactInfo contactInfo = new ContactInfo
            {
                //Phone = "", // Changed from the ERD
                Address1 = "",
                CityId = -1,
                ProvinceId = -1,
                UserId = userId

            };
            _db.ContactInfo.Add(contactInfo);
            _db.SaveChanges();
            return contactInfo;
        }

        public void UpdateContactInfo(ContactInfoVM contactInfo)
        {
            ContactInfo? contact = _db.ContactInfo.Find(contactInfo.ContactId);
            if (contact != null)
            {
                contact.Address1 = contactInfo.Address1;
                contact.Address2 = contactInfo.Address2;
                contact.CityId = contactInfo.CityId;
                contact.ProvinceId = contactInfo.ProvinceId;
                _db.SaveChanges();
            }
        }

        public void DeleteContactInfo(int contactId)
        {
            ContactInfo? contact = _db.ContactInfo.Find(contactId);
            if (contact != null)
            {
                _db.ContactInfo.Remove(contact);
                _db.SaveChanges();
            }
        }
    }
}