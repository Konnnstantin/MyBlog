using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;
        private IMapper _mapper;
        public RoleController(IMapper mapper, IRoleService roleService, ILogger<RoleController> logger)
        {
            _mapper = mapper;
            _roleService = roleService;
            _logger = logger;
        }


        [Route("Role/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// Метод получения роли
        /// </summary>
        /// <param name="Модель создания роли"></param>
        /// <returns></returns>
        [Route("Role/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleId = await _roleService.CreateRole(model);
                _logger.LogInformation($"Созданна роль - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("Role/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult EditRole(Guid id)
        {
            var view = new RoleEditViewModel { Id = id };
            return View(view);
        }
        /// <summary>
        /// Метод редактриования роли
        /// </summary>
        /// <param name="Модель для редактирования роли"></param>
        /// <returns></returns>
        [Route("Role/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleService.EditRole(model);
                _logger.LogDebug($"Измененна роль - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }
        
        [Route("Role/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult RemoveRole() => View("Index");

        /// <summary>
        /// Метод удаления роли
        /// </summary>
        /// <param name="id роли"></param>
        /// <returns></returns>
        [Route("Role/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await _roleService.DeleteRole(id);
            _logger.LogDebug($"Удаленна роль - {id}");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Метод получения всех ролей
        /// </summary>
        /// <returns></returns>
        [Route("Role/GetRoles")]
        [HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return View(roles);
        }
    }
}
