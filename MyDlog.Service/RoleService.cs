using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private IMapper _mapper;
        private readonly IUnitOfwork _unitOfWork;
        public RoleService(IMapper mapper, RoleManager<Role> roleManager, IUnitOfwork unitOfwork)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }
        public async Task<Guid> CreateRole(RoleCreateViewModel model)
        {
            var role = new Role() { Name = model.Name, Description = model.Description };
            await _roleManager.CreateAsync(role);
            await _unitOfWork.Save();

            return Guid.Parse(role.Id);
        }
        public async Task EditRole(RoleEditViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) && model.Description == null)
                return;

            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            if (!string.IsNullOrEmpty(model.Name))
                role.Name = model.Name;
            if (model.Description != null)
                role.Description = model.Description;

            await _roleManager.UpdateAsync(role);
            await _unitOfWork.Save();
        }

        public async Task DeleteRole(Guid Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            await _roleManager.DeleteAsync(role);
            await _unitOfWork.Save();   
        }

        public async Task<List<Role>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}
