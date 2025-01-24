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

        public IEnumerable<City> GetAllCities()
        {
            IEnumerable<City> cities = _db.City.ToList();
            return cities;
        }

        public IEnumerable<Province> GetAllProvinces()
        {
            IEnumerable<Province> provinces = _db.Province.ToList();
            return provinces;
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

        public SelectList GetCitySelectList()
        {
            IEnumerable<SelectListItem> cities = GetAllCities().Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.Name
            });
            SelectList citySelectList = new SelectList(cities, "Value", "Text");
            return citySelectList;
        }

        public SelectList GetProvinceSelectList()
        {
            IEnumerable<SelectListItem> provinces = GetAllProvinces().Select(p => new SelectListItem
            {
                Value = p.ProvinceId.ToString(),
                Text = p.Name
            });
            SelectList provinceSelectList = new SelectList(provinces, "Value", "Text");
            return provinceSelectList;
        }
    }
}