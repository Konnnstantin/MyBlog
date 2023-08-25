using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        /// <summary>
        /// Добавление тега
        /// </summary>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddTag(TagCreateViewModel model)
        {
            await _tagService.CreateTag(model);
            return StatusCode(200);
        }
        /// <summary>
        /// Удаление тега
        /// </summary>
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.DeleteTag(id);
            return StatusCode(200);
        }
        /// <summary>
        /// Редактирование тега
        /// </summary>
        /// <param name="model тэга"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPatch]
        public async Task<IActionResult> EditTag(TagEditViewModel model)
        {
            await _tagService.EditTag(model);
            _logger.LogInformation($"Изменен тег - {model.PosTag}");
            return StatusCode(200);

        }
        /// <summary>
        /// Метод полученния тегов
        /// </summary>
        /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<Tag>> GetTags()
        {
            var tags = await _tagService.GetTags();
            return tags;
        }
    }
}
