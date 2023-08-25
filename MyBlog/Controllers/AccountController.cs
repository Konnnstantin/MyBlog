using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
     
        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Метод входа в аккаунт
        /// </summary>
        /// <param name="Модель логина"></param>
        /// <returns></returns>
        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Вход в аккаунт {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        /// <summary>
        /// Метод выхода из аккаунта
        /// </summary>
        /// <returns></returns>
        [Route("Account/Logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutAccount()
        {
            await _accountService.LogoutAccount();
            return RedirectToAction("Index", "Home");
        }


        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register() => View();
        /// <summary>
        /// Метод Регистрации
        /// </summary>
        /// <param name="Модель регистрации"></param>
        /// <returns></returns>
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.Register(model);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Создан аккаунт - {model.Email}");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Метод получения аккаунта
        /// </summary>
        /// <returns></returns>
        [Route("Account/Get")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }

        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(Guid id)
        {
            var model = await _accountService.EditAccount(id);
            return View(model);
        }

        /// <summary>
        /// Метод редактирования аккаунта
        /// </summary>
        /// <param name="Модель для редактирования аккаунта"></param>
        /// <returns></returns>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(AccountEditViewModel model)
        {
            try
            {
                var result = await _accountService.EditAccount(model);

                if (result.Succeeded)
                {
                    _logger.LogDebug($"Аккаунт - {model.UserName} был изменен");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", $"{result.Errors.First().Description}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }
        }
        /// <summary>
        /// Метод удаления аккакутна
        /// </summary>
        /// <param name="id аккаунта"></param>
        /// <param name="confirm свойство проверки"></param>
        /// <returns></returns>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveAccount(id);
            return RedirectToAction("Index", "Home");
        }

    }
}
