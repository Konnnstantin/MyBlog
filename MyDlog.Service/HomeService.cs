using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class HomeService : IHomeService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public IMapper _mapper;

        public HomeService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task GenerateData()
        {
            var testUser = new RegisterViewModel { UserName = "Test", Email = "Test@gmail.com", Password = "1234aB", FirstName = "Test", LastName = "Testov" };
            var testUser2 = new RegisterViewModel { UserName = "Test2", Email = "Test2@gmail.com", Password = "12342aB", FirstName = "Test2", LastName = "Testov2" };
            var testUser3 = new RegisterViewModel { UserName = "Test3", Email = "Test3@gmail.com", Password = "12343aB", FirstName = "Test3", LastName = "Testov3" };

            var user = _mapper.Map<User>(testUser);
            var user1 = _mapper.Map<User>(testUser2);
            var user2 = _mapper.Map<User>(testUser3);

            var userRole = new Role() { Name = "Пользователь", Description = "Стандартные настройки" };
            var moderRole = new Role() { Name = "Модератор", Description = "Стандартные модератор" };
            var adminRole = new Role() { Name = "Администратор", Description = "стандартные администратор" };

            await _userManager.CreateAsync(user, testUser.Password);
            await _userManager.CreateAsync(user1, testUser2.Password);
            await _userManager.CreateAsync(user2, testUser3.Password);

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(moderRole);
            await _roleManager.CreateAsync(adminRole);

            await _userManager.AddToRoleAsync(user, userRole.Name);
            await _userManager.AddToRoleAsync(user1, moderRole.Name);
            await _userManager.AddToRoleAsync(user2, adminRole.Name);
        }
    }
}
