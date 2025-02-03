using PaaS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using PaaS.ViewModels;
using PaaS.Data;

namespace PaaS.Repositories
{
    public class ContactInfoRepo
    {
        private readonly ApplicationDbContext _context;

        public ContactInfoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get the contact info for a user
        public ContactInfo? GetUserContactInfo(int userId)
        {
            var contactInfo = _context.ContactInfos
                .Where(ci => ci.UserId == userId)
                .Select(ci => new ContactInfo
                {
                    ContactId = ci.ContactId,
                    Phone = ci.Phone,
                    Address1 = ci.Address1,
                    Address2 = ci.Address2,
                    CityId = ci.CityId,
                    ProvinceId = ci.ProvinceId,
                    City = ci.City != null ? new City { CityId = ci.City.CityId, Name = ci.City.Name } : null,
                    Province = ci.Province != null ? new Province { ProvinceId = ci.Province.ProvinceId, Name = ci.Province.Name } : null
                })
                .FirstOrDefault();

            return contactInfo;
        }

        // Get the user's address book
        public List<ContactInfo> GetUserAddressBook(int userId)
        {
            var addressBook = _context.ContactInfos
                .Where(ci => ci.UserId == userId)
                .Select(ci => new ContactInfo
                {
                    ContactId = ci.ContactId,
                    Phone = ci.Phone,
                    Address1 = ci.Address1,
                    Address2 = ci.Address2,
                    CityId = ci.CityId,
                    ProvinceId = ci.ProvinceId,
                    City = ci.City != null ? new City { CityId = ci.City.CityId, Name = ci.City.Name } : null,
                    Province = ci.Province != null ? new Province { ProvinceId = ci.Province.ProvinceId, Name = ci.Province.Name } : null
                })
                .ToList();

            return addressBook;
        }

        // Get a contact info by its ID
        public ContactInfo? GetContactInfoById(int contactId)
        {
            var contactInfo = _context.ContactInfos
                .Where(ci => ci.ContactId == contactId)
                .Select(ci => new ContactInfo
                {
                    ContactId = ci.ContactId,
                    Phone = ci.Phone,
                    Address1 = ci.Address1,
                    Address2 = ci.Address2,
                    CityId = ci.CityId,
                    ProvinceId = ci.ProvinceId,
                    City = ci.City != null ? new City { CityId = ci.City.CityId, Name = ci.City.Name } : null,
                    Province = ci.Province != null ? new Province { ProvinceId = ci.Province.ProvinceId, Name = ci.Province.Name } : null
                })
                .FirstOrDefault();

            return contactInfo;
        }
    }
}