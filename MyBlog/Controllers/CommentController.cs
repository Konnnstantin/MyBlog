using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.DAL.Repository;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;

namespace MyBlog.Controllers
{
    public class CommentController : Controller
    {
        private IMapper _mapper;
        private IUnitOfwork _unitOfwork;
        private ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(IMapper mapper, IUnitOfwork unitOfwork, ICommentService commentService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfwork = unitOfwork;
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Comment/Create")]
        public IActionResult CreateComment(Guid articleId)
        {
            var model = new CommentCreateViewModel() { ArticleId = articleId };
            return View(model);
        }

        /// <summary>
        /// Метод создания комментария
        /// </summary>
        /// <param name="model создания комментария"></param>
        /// <param name="ArticleId статьи"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Comment/Create")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model, Guid ArticleId)
        {
            model.ArticleId = ArticleId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _commentService.CreateComment(model, new Guid(user.Id));
            return RedirectToAction("Index", "Home");
        }

        [Route("Comment/Edit")]
        [HttpGet]
        public IActionResult EditComment(Guid id)
        {
            var view = new CommentEditViewModel { Id = id };
            return View(view);
        }

        /// <summary>
        /// Метод для редактирования комментария
        /// </summary>
        /// <param name="model редактирования комментария"></param>
        /// <returns></returns>
        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }


        [HttpGet]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Метод для удаления коментария
        /// </summary>
        /// <param name="id комментария"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
