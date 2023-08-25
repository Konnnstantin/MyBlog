using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Введите роль")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
