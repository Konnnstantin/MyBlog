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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Comment entity)
        {
            await _db.Comments.AddAsync(entity);
        }
        public async Task Delete(Guid id)
        {
            _db.Comments.Remove(Get(id));
        }
        public Comment Get(Guid id)
        {
            return _db.Comments.FirstOrDefault(_=>_.Id == id);
        }
        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments;
        }

        public async Task<Comment> Update(Comment entity)
        {
            _db.Comments.Update(entity);
            return entity;
        }
        public List<Comment> GetCommentsByArticleId(Guid id)
        {
            return _db.Comments.Where(c => c.ArticleId == id).ToList();
        }
    }
}

