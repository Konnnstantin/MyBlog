using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        public IMapper _mapper;
        public AccountService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AccountEditViewModel> EditAccount(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = _roleManager.Roles.ToList();
            var model = new AccountEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                UserName = user.UserName,
                Email = user.Email,
                Password = string.Empty,
                Id = id,
                Roles = roles.Select(_ => new RoleViewModel() { Id = new Guid(_.Id), Name = _.Name }).ToList()
            };
            return model;
        }

        public async Task<IdentityResult> EditAccount(AccountEditViewModel model)
        {

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            
             user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Age = model.Age;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);


            foreach (var role in model.Roles)
            {
                var roleName = _roleManager.FindByIdAsync(role.Id.ToString()).Result.Name;
                if (role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                return result;
        }

        public async Task LogoutAccount()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var role = new Role() { Name = "User", Description = "Пользовательские права доступа" };
                await _roleManager.CreateAsync(role);
                var currentUser = await _userManager.FindByIdAsync(user.Id);
                await _userManager.AddToRoleAsync(currentUser, role.Name);

                return result;

            }
            else
            {
                return result;
            }
        }
        public async Task<List<User>> GetAccounts()
        {

            var accounts = _userManager.Users.Include(u => u.Articles).ToList();

            foreach (var user in accounts)
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    var newRole = new Role { Name = role };
                    user.Roles.Add(newRole);
                }
            }
            return accounts;
        }
        public async Task RemoveAccount(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            await _userManager.DeleteAsync(user);
        }
    }
}
