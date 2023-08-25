using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System.Runtime.CompilerServices;

namespace MyBlog.Controllers
{
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
        /// Метод получения статьи
        /// </summary>
        /// <param name="id статьи"></param>
        /// <returns></returns>
        [Route("Article/Get")]
        [HttpGet]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var post = await _articleService.Get(id);
            return View(post);
        }

        [Route("Article/Create")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateArticle()
        {
            var model = await _articleService.CreateArticle();
            return View(model);
        }
        /// <summary>
        /// Метод для создания статьи
        /// </summary>
        /// <param name="model создания статьи"></param>
        /// <returns></returns>
        [Route("Article/Create")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.AuthorId = user.Id;

            if (string.IsNullOrEmpty(model.Header) || string.IsNullOrEmpty(model.Content))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            var postId = await _articleService.CreateArticle(model);
            return RedirectToAction("Index", "Home");
        }


        [Route("Article/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditArticle(Guid id)
        {
            var model = await _articleService.EditArticle(id);
            return View(model);
        }

        /// <summary>
        /// Метод для редактирования статьи
        /// </summary>
        /// <param name="model редактирования статьи"></param>
        /// <param name="Id статьи"></param>
        /// <returns></returns>
        [Authorize]
        [Route("Article/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Header) || string.IsNullOrEmpty(model.Content))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }
            await _articleService.EditArticle(model, Id);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("Article/Remove")]
        public async Task<IActionResult> RemoveArticle(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveArticle(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Метод для удаления статьи
        /// </summary>
        /// <param name="id статьи"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Article/Remove")]
        public async Task<IActionResult> RemoveArticle(Guid id)
        {
            await _articleService.Remove(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Метод получения всех статей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Article/GetAll")]
        public async Task<IActionResult> GetAllArticle()
        {
            var posts = await _articleService.GetAll();

            return View(posts);
        }
    }
}
