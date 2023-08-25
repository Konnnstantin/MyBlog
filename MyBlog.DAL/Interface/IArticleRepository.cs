using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Interface
{
    public interface IArticleRepository : IRepository<Article>
    {
        public  Task Create(Article entity);
        public  Task Delete(Guid id);
        public Article Get(Guid id);
        public IEnumerable<Article> GetAll();
        public  Task<Article> Update(Article entity);
      
    }
}
