using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using MyBlog.DAL.Interface;
using MyBlog.DAL.Repository;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;

        public ArticleService(IUnitOfwork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<ArticleCreateViewModel> CreateArticle()
        {
            var article = new Article();
            var tags = _unitOfWork.Tags.GetAll().Select(_ => new TagViewModel() { Id = _.Id, PosTag = _.PostTag }).ToList();
            

            var model = new ArticleCreateViewModel
            {
                Header = article.Header,
                Content = article.Content,
                Tags = tags,

            };
            return model;
        }

        public async Task<Guid> CreateArticle(ArticleCreateViewModel model)
        {
            var dbTags = new List<Tag>();
            if (model.Tags != null)
            {
                var articleTags = model.Tags.Where(_ => _.IsSelected == true).ToList();
                var articleId = articleTags.Select(_ => _.Id).ToList();
                dbTags = _unitOfWork.Tags.GetAll().Where(_ => articleId.Contains(_.Id)).ToList();
            }
            Article article = new Article()
            {
                Id = model.Id,
                Header = model.Header,
                Content = model.Content,
                Tags = dbTags,
                AuthorId = model.AuthorId,
            };

            var user = await _userManager.FindByIdAsync(model.AuthorId);
            user.Articles.Add(article);
            await _unitOfWork.Articles.Create(article);
            await _userManager.UpdateAsync(user);
            await _unitOfWork.Save();
            return article.Id;
        }

        public async Task<ArticleEditViewModel> EditArticle(Guid id)
        {
            var article = _unitOfWork.Articles.Get(id);

            var tags = _unitOfWork.Tags.GetAll().Select(t => new TagViewModel() { Id = t.Id, PosTag = t.PostTag }).ToList();

            foreach (var tag in tags)
            {
                foreach (var postTag in article.Tags)
                {
                    if (postTag.Id == tag.Id)
                    {
                        tag.IsSelected = true;
                        break;
                    }
                }
            }
            var model = new ArticleEditViewModel()
            {
                Id = id,
                Header = article.Header,
                Content = article.Content,
                Tags = tags
            };
            return model;
        }

        public async Task EditArticle(ArticleEditViewModel model, Guid Id)
        {
            var article = _unitOfWork.Articles.Get(Id);
            if (article != null)
            {
                article.Header = model.Header;
                article.Content = model.Content;
            }

            foreach (var tag in model.Tags)
            {
                var tagChanged = _unitOfWork.Tags.Get(tag.Id);
                if (tag.IsSelected)
                {
                    article.Tags.Add(tagChanged);
                }
                else
                {
                    article.Tags.Remove(tagChanged);
                }
            }

            await _unitOfWork.Articles.Update(article);
            await _unitOfWork.Save();
        }

        public async Task<Article> Get(Guid id)
        {
            var article = _unitOfWork.Articles.Get(id);
            var user = await _userManager.FindByIdAsync(article.AuthorId);

            var comments = _unitOfWork.Comments.GetCommentsByArticleId(id);
            article.Id = id;

            foreach (var comment in comments)
            {
                if (article.Comments.FirstOrDefault(_ => _.Id == comment.Id) == null)
                {
                    article.Comments.Add(comment);
                }
            }
            article.AuthorId = user.UserName;
            return article;
        }


        public async Task<List<Article>> GetAll()
        {
            var articles = _unitOfWork.Articles.GetAll().ToList();

            return articles;
        }

        public async Task Remove(Guid id)
        {
            await _unitOfWork.Articles.Delete(id);
            await _unitOfWork.Save();
        }
    }
}
