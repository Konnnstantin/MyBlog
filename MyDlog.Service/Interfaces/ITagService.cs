using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Interfaces
{
    public interface ITagService
    {
        Task<Guid> CreateTag(TagCreateViewModel model);
        Task EditTag(TagEditViewModel model);
        Task DeleteTag(Guid id);
        Task<List<Tag>> GetTags();
    }
}
