using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class CommentCreateViewModel
    {
        [Required(ErrorMessage = "Введите текст")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст")]
        public string? Text { get; set; }
        public Guid ArticleId;
    }
}
