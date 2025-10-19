using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface ITagRepository
    {
        // Lấy tất cả tag
        Task<IEnumerable<Tag>> GetAllAsync();

        // Lấy tag theo ID
        Task<Tag?> GetByIdAsync(Guid id);

        // Lấy tag theo slug
        Task<Tag?> GetBySlugAsync(string slug);

        // Tạo mới
        Task<Tag> CreateAsync(Tag tag);

        // Cập nhật
        Task UpdateAsync(Tag tag);

        // Xóa
        Task DeleteAsync(Guid id);

        // Kiểm tra trùng tên hoặc slug
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsBySlugAsync(string slug);
    }
}
