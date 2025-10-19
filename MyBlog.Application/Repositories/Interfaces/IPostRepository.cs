using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface IPostRepository
    {
        // Lấy tất cả bài viết
        Task<IEnumerable<Post>> GetAllAsync(bool includeRelations = false);

        // Lấy 1 bài viết theo ID
        Task<Post?> GetByIdAsync(Guid id, bool includeRelations = false);

        // Lấy 1 bài viết theo Slug
        Task<Post?> GetBySlugAsync(string slug, bool includeRelations = false);

        // Lấy bài viết theo Category
        Task<IEnumerable<Post>> GetByCategoryAsync(Guid categoryId);

        // Lấy bài viết theo User (Author)
        Task<IEnumerable<Post>> GetByAuthorAsync(Guid authorId);

        // Tạo mới bài viết
        Task<Post> CreateAsync(Post post);

        // Cập nhật
        Task UpdateAsync(Post post);

        // Xóa
        Task DeleteAsync(Guid id);

        // Kiểm tra trùng Slug
        Task<bool> ExistsBySlugAsync(string slug);
    }
}
