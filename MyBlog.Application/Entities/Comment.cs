using System;

namespace MyBlog.Application.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
    }
}
