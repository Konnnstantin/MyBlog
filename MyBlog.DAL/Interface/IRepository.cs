using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Interface
{
    public interface IRepository<T> where T :class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        Task Create(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid id);
       
    }
}
