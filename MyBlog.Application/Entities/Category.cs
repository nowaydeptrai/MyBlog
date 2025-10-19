using System;
using System.Collections.Generic;

namespace MyBlog.Application.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();  
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
        public string? Description { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
