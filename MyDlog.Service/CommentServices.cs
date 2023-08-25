using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Domains.ViewModels;

namespace MyBlog.Service
{
    public class CommentServices : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfwork _unitOfWork;
        private UserManager<User> _userManager;

        public CommentServices(IMapper mapper, IUnitOfwork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Guid> CreateComment(CommentCreateViewModel model, Guid UserId)
        {
            var comment = new Comment
            {
                Text = model.Text,
                ArticleId = model.ArticleId,
                AuthorId = UserId
            };
            await _unitOfWork.Comments.Create(comment);
            await _unitOfWork.Save();
            return comment.Id;
        }

        public async Task<Guid> CreateComment(CommentCreateViewModel model)
        {
            var comment = new Comment
            {
                Text = model.Text,
                ArticleId = model.ArticleId,
                AuthorId = Guid.Empty,
            };
            await _unitOfWork.Comments.Create(comment);
            await _unitOfWork.Save();
            return comment.Id;
        }

        public async Task EditComment(CommentEditViewModel model)
        {
            var comment = _unitOfWork.Comments.Get(model.Id);
            comment.Text = model.Text;
            await _unitOfWork.Comments.Update(comment);
            await _unitOfWork.Save();
        }

        public async Task<List<Comment>> GetComments()
        {
            return _unitOfWork.Comments.GetAll().ToList();
        }

        public async Task RemoveComment(Guid id)
        {
            await _unitOfWork.Comments.Delete(id);
            await _unitOfWork.Save();
        }
    }
}
