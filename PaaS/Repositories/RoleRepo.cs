using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.ViewModels;

namespace PaaS.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _db;
        public RoleRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            var roles = _db.Roles.ToList();
            return roles;
        }

        public IEnumerable<RoleVM> GetAllRolesVM()
        {
            var roles =
            _db.Roles.Select(r => new RoleVM
            {
                RoleName = r.Name
            }).ToList();
            return roles;
        }

        public IdentityRole? GetRole(string roleName)
        {
            IdentityRole? role =
            _db.Roles.FirstOrDefault(r => r.Name == roleName);
            return role;
        }

        public RoleVM? GetRoleVM(string roleName)
        {
            IdentityRole? role = GetRole(roleName);
            if (role != null)
            {
                return new RoleVM { RoleName = role.Name };
            }
            return null;
        }

        public bool DoesRoleHaveUsers(string roleName)
        {
            IdentityRole? role = GetRole(roleName);
            if (role == null)
            {
                return false;
            }
            return
            _db.UserRoles.Any(r => r.RoleId == role.Id);
        }

        public SelectList GetRoleSelectList()
        {
            var roles = GetAllRoles().Select(r =>
            new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            SelectList roleSelectList =
            new SelectList(roles, "Value", "Text");
            return roleSelectList;
        }

    }
}