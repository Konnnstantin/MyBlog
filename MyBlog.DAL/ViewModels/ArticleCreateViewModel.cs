using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class ArticleCreateViewModel
    {
        public Guid Id { get; set; }
        public string? AuthorId { get; set; }
        public List<TagViewModel>? Tags { get; set; }


        [Required(ErrorMessage = "Введите заголовок")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок")]
        public string? Header { get; set; }

        [Required(ErrorMessage = "Введите текст")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст")]
        public string? Content { get; set; }
    }
}
