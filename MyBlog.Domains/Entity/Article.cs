using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Domains.Entity
{
    public class Article
    {
        public Guid Id { get; set; }
        public string? Header { get; set; }
        public string? Content { get; set; }
        public string? AuthorId { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
