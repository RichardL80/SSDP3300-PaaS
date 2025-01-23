using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.ViewModels;
using PaaS.Models;

namespace PaaS.Repositories
{
    public class LocationRepo
    {
        private readonly ApplicationDbContext _db;

        public LocationRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public string GetCityName(int cityId)
        {
            City? city = _db.City.Find(cityId);
            return city?.Name ?? "Unknown City";
        }

        public string GetProvinceName(int provinceId)
        {
            Province? province = _db.Province.Find(provinceId);
            return province?.Name ?? "Unknown Province";
        }
    }
}