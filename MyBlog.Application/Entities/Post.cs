using System;
using System.Collections.Generic;

namespace MyBlog.Application.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public string Slug { get; set; } = "";
        public string Content { get; set; } = "";
        public string? ThumbnailUrl { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
