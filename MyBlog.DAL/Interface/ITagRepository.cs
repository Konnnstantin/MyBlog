using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Interface
{
    public interface ITagRepository : IRepository<Tag>
    {
        public Task Create(Tag entity);
        public Task Delete(Guid id);
        public Tag Get(Guid id);
        public IEnumerable<Tag> GetAll();
        public Task<Tag> Update(Tag entity);
      
    }
}
