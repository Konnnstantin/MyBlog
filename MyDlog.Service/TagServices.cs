using AutoMapper;
using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using MyBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class TagServices : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfwork _unitOfWork;

        public TagServices(IMapper mapper, IUnitOfwork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }

        public async Task<Guid> CreateTag(TagCreateViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _unitOfWork.Tags.Create(tag);
            await _unitOfWork.Save();

            return tag.Id;
        }

        public async Task DeleteTag(Guid id)
        {
            await _unitOfWork.Tags.Delete(id);
            await _unitOfWork.Save();
        }

        public async Task EditTag(TagEditViewModel model)
        {
            var tag = _unitOfWork.Tags.Get(model.Id);
            tag.PostTag = model.PosTag;
            await _unitOfWork.Tags.Update(tag);
            await _unitOfWork.Save();
        }

        public async Task<List<Tag>> GetTags()
        {
            return _unitOfWork.Tags.GetAll().ToList();
        }
    }
}
