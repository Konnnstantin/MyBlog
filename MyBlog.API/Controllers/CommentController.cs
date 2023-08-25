using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System.Collections;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        /// <summary>
        /// Метод получения комментарний
        /// </summary>
        /// <param name="id комментария"></param>
        /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comment = await _commentService.GetComments();
            return comment;
        }

        /// <summary>
        /// Метод создания комментария
        /// </summary>
        /// <param name="model создания комментария"></param>
        /// <param name="ArticleId статьи"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model, Guid ArticleId)
        {
            model.ArticleId = ArticleId;
            await _commentService.CreateComment(model, ArticleId);
            return StatusCode(200);
        }

        /// <summary>
        /// Метод для редактирования комментария
        /// </summary>
        /// <param name="model редактирования комментария"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPatch]
        public async Task<IActionResult> EditComment(CommentEditViewModel model)
        {
            if (model != null)
            {
                await _commentService.EditComment(model);
                return StatusCode(200);
            }
            else
            {
                return  BadRequest();
            }
        }

        /// <summary>
        /// Метод для удаления коментария
        /// </summary>
        /// <param name="id комментария"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            return StatusCode(200);
        }
    }
}

