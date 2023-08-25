using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private IMapper _mapper;
        private readonly IUnitOfwork _unitOfwork;
        private readonly UserManager<User> _userManager;
        public ArticleController(IArticleService articleService, IMapper mapper, IUnitOfwork unitOfwork, UserManager<User> userManager)
        {
            _articleService = articleService;
            _mapper = mapper;
            _unitOfwork = unitOfwork;
            _userManager = userManager;
        }

        /// <summary>
        /// Метод для создания статьи
        /// </summary>
        /// <param name="model создания статьи"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateViewModel model)
        {
            await _articleService.CreateArticle(model);
            return StatusCode(200);
        }

        /// <summary>
        /// Метод для редактирования статьи
        /// </summary>
        /// <param name="model редактирования статьи"></param>
        /// <param name="Id статьи"></param>
        /// <returns></returns>
       /// [Authorize]
        [Route("Edit")]
        [HttpPatch]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Header) || string.IsNullOrEmpty(model.Content))
            {
                StatusCode(200);
            }
            await _articleService.EditArticle(model, Id);
            return StatusCode(204);
        }

        /// <summary>
        /// Метод для удаления статьи
        /// </summary>
        /// <param name="id статьи"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveArticle(Guid id)
        {
            await _articleService.Remove(id);
            return StatusCode(200);
        }

        /// <summary>
        /// Метод получения всех статей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Article>> GetAllArticle()
        {
            var posts = await _articleService.GetAll();
            return posts.ToList();
        }
    }
}
