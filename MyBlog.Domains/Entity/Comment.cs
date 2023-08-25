using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.Entity
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public Guid? ArticleId { get; set; }
        public Guid? AuthorId { get; set; }
        
    }
}
