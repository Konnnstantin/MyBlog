using Microsoft.EntityFrameworkCore;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Article entity)
        {
            await _db.Articles.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            _db.Articles.Remove(Get(id));
        }

        public  Article Get(Guid id)
        {
           return  _db.Articles.Include(_ => _.Tags).FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _db.Articles.Include(_=>_.Tags).ToList();
        }

        public async Task<Article> Update(Article entity)
        {
            _db.Articles.Update(entity);
            return entity;
        }
    }
}

