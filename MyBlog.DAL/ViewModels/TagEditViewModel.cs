using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class TagEditViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Введите текст")]
        [DataType(DataType.Text)]
        [Display(Name = "Название")]
        public string? PosTag { get; set; }

    }
}
