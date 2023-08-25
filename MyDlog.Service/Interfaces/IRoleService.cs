using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Interfaces
{
    public interface IRoleService
    {
        Task<Guid> CreateRole(RoleCreateViewModel model);
        Task EditRole(RoleEditViewModel model);
        Task DeleteRole(Guid id);
        Task<List<Role>> GetRoles();

    }
}
