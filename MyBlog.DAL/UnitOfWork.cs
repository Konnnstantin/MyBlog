using MyBlog.DAL.Interface;
using MyBlog.DAL.Repository;
using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly ApplicationDbContext _db;
        private ITagRepository _tagRepository;
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;
        private bool disposed = false;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public IArticleRepository Articles
        {
            get
            {
                if (_articleRepository == null)
                    _articleRepository = new ArticleRepository(_db);
                return _articleRepository;
            }
        }
        public ITagRepository Tags
        {
            get
            {
                if (_tagRepository == null)
                    _tagRepository = new TagRepository(_db);
                return _tagRepository;
            }
        }
        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_db);
                return _commentRepository;
            }
        }

        public async Task Save()
        {
           await _db.SaveChangesAsync();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
