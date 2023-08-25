using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Введите текст")]
        [Display(Name = "Name")]
        public string? PosTag { get; set; }
        public bool IsSelected { get; set; }
    }
}
