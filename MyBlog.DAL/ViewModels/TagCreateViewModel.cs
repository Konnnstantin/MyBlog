using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Введите текст")]
        [DataType(DataType.Text)]
        [Display(Name = "Название")]
        public string? PostTag { get; set; }
    }
}
