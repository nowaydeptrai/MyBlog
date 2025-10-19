using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        // Lấy tất cả comment
        Task<IEnumerable<Comment>> GetAllAsync();

        // Lấy comment theo ID
        Task<Comment?> GetByIdAsync(Guid id);

        // Lấy tất cả comment của 1 bài viết
        Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);

        // Lấy tất cả comment của 1 user
        Task<IEnumerable<Comment>> GetByUserIdAsync(Guid userId);

        // Lấy phản hồi (reply) của 1 comment
        Task<IEnumerable<Comment>> GetRepliesAsync(Guid parentCommentId);

        // Tạo mới
        Task<Comment> CreateAsync(Comment comment);

        // Cập nhật
        Task UpdateAsync(Comment comment);

        // Xóa
        Task DeleteAsync(Guid id);
    }
}
