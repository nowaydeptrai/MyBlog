using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        // Lấy tất cả category
        Task<IEnumerable<Category>> GetAllAsync();

        // Lấy category theo ID
        Task<Category?> GetByIdAsync(Guid id);

        // Lấy category theo Slug
        Task<Category?> GetBySlugAsync(string slug);

        // Tạo mới
        Task<Category> CreateAsync(Category category);

        // Cập nhật
        Task UpdateAsync(Category category);

        // Xóa
        Task DeleteAsync(Guid id);

        // Kiểm tra trùng tên hoặc slug
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsBySlugAsync(string slug);
    }
}
