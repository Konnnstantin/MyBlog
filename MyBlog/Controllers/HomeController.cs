using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Models;
using MyBlog.Service.Interfaces;
using System.Diagnostics;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHomeService _homeService;
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;

        public HomeController(RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IHomeService homeService, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _homeService = homeService;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Метод получения домашней страницы
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            await _homeService.GenerateData();

            return View(new MainViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Метод исключения
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        [Route("Home/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    _logger.LogWarning($"Произошла ошибка - {statusCode}\n{viewName}");
                    return View(viewName);
                }
                else if (statusCode == 403)
                {
                    return View("403");
                }
            }
            return View("500");
        }
    }
}