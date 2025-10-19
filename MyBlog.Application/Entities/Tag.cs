using System;
using System.Collections.Generic;

namespace MyBlog.Application.Entities
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
