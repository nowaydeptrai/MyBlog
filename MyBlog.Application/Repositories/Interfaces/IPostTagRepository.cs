using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface IPostTagRepository
    {
        // Lấy tất cả tag của 1 bài viết
        Task<IEnumerable<Tag>> GetTagsByPostIdAsync(Guid postId);

        // Lấy tất cả bài viết có 1 tag cụ thể
        Task<IEnumerable<Post>> GetPostsByTagIdAsync(Guid tagId);

        // Gán tag cho bài viết
        Task AddTagToPostAsync(Guid postId, Guid tagId);

        // Gỡ tag khỏi bài viết
        Task RemoveTagFromPostAsync(Guid postId, Guid tagId);

        // Kiểm tra xem bài viết đã có tag đó chưa
        Task<bool> ExistsAsync(Guid postId, Guid tagId);
    }
}
