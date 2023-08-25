using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleCreateViewModel> CreateArticle();
        Task<Guid> CreateArticle(ArticleCreateViewModel model);
        Task<ArticleEditViewModel> EditArticle(Guid Id);
        Task EditArticle(ArticleEditViewModel model, Guid Id);
        Task Remove(Guid id);
        Task<List<Article>> GetAll();
        Task<Article> Get(Guid id);
    }
}
