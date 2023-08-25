using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.DAL.Repository;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using MyBlog.Service;

namespace MyBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly ILogger<TagController> _logger;
        private IMapper _mapper;
        private readonly ITagService _tagService;
        public TagController(IUnitOfwork unitOfwork, IMapper mapper, ILogger<TagController> logger, ITagService tagService)
        {
            _mapper = mapper;
            _unitOfwork = unitOfwork;
            _logger = logger;
            _tagService = tagService;
        }


        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }

        /// <summary>
        /// Метод создания Тега 
        /// </summary>
        /// <param name="model создания тега"></param>
        /// <returns></returns>
        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.CreateTag(model);
                _logger.LogInformation($"Создан тег - {model.PostTag}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult EditTag(Guid id)
        {
            var view = new TagEditViewModel { Id = id };
            return View(view);
        }

        /// <summary>
        /// Метод редактирования тега
        /// </summary>
        /// <param name="model для редактирования тега"></param>
        /// <returns></returns>
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model);
                _logger.LogInformation($"Изменен тег - {model.PosTag}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveTag(Guid id, bool isConfirm = true)
        {
            if (isConfirm)
                await RemoveTag(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Метод удаления тега
        /// </summary>
        /// <param name="id тега"></param>
        /// <returns></returns>
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.DeleteTag(id);
            _logger.LogInformation($"Удаленн тег - {id}");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Метод полученния тегов
        /// </summary>
        /// <returns></returns>
        [Route("Tag/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
    }
}
