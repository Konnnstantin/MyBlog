using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System.Security.Claims;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        /// <summary>
        /// Метод входа в аккаунт
        /// </summary>
        /// <param name="Модель логина"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                await _accountService.Login(model);
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

                if (roles.Contains("Администратор"))
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Администратор"));
                }
                else
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.First()));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

             

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                _logger.LogInformation($"Вход в аккаунт {model.Email}");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                await _accountService.Register(model);
                _logger.LogInformation($"Создан аккаунт - {model.Email}");
                return StatusCode(200);
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
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAccounts();
                return accounts.ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(ex.Message));
            }

        }


        /// <summary>
        /// Метод редактирования аккаунта
        /// </summary>
        /// <param name="Модель для редактирования аккаунта"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPatch]
        public async Task<IActionResult> EditAccount(AccountEditViewModel model)
        {
            try
            {
                await _accountService.EditAccount(model);
                _logger.LogDebug($"Аккаунт - {model.UserName} был изменен");
                return StatusCode(200);
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
        /// <returns></returns>
        [Route("Remove")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            try
            {
                await RemoveAccount(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
