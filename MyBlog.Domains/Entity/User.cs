using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.Entity
{
    public class User :IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
