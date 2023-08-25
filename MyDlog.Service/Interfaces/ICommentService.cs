using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Interfaces
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentCreateViewModel model, Guid UserId);
        Task EditComment(CommentEditViewModel model);
        Task RemoveComment(Guid id);
        Task<List<Comment>> GetComments();
    }
}
