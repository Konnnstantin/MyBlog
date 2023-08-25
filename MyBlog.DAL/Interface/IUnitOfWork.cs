using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Interface
{
    public interface IUnitOfwork : IDisposable
    {
        public IArticleRepository Articles { get; }
        public ITagRepository Tags { get; }
        public Task Save();
        public ICommentRepository Comments { get; }
    }
}
