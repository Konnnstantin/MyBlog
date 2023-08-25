using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.ViewModels
{
    public class RoleEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите роль")]
        [DataType(DataType.Text)]
        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Описание роли")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
