using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetAll();
        Comment Get(Guid id);
        Task Create(Comment entity);
        Task<Comment> Update(Comment entity);
        Task Delete(Guid id);
        List<Comment> GetCommentsByArticleId(Guid id);
    }
}
