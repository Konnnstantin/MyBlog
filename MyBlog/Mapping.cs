using AutoMapper;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));
            CreateMap<AccountEditViewModel, User>();
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<TagEditViewModel, Tag>();
            CreateMap<ArticleCreateViewModel, Article>();
            CreateMap<ArticleEditViewModel, Article>();
            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<CommentEditViewModel, Comment>();

        }
    }
}
