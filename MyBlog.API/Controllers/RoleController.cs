using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        /// <summary>
        /// Получение всех ролей
        /// </summary>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return roles;
        }

        /// <summary>
        /// Добавление роли
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel request)
        {
            await _roleService.CreateRole(request);
            return StatusCode(200);
        }

        /// <summary>
        /// Редактирование роли
        /// </summary>
        [HttpPatch]
        [Route("Edit")]
        public async Task<IActionResult> EditRole(RoleEditViewModel request)
        {
            await _roleService.EditRole(request);

            return StatusCode(200);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await _roleService.DeleteRole(id);

            return StatusCode(200);
        }
    }
}
