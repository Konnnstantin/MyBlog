using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.Entity
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string? PostTag { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
