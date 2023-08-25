using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Введите роль")]
        [DataType(DataType.Text)]
        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Введите описание роли")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
