using MyBlog.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // Lấy toàn bộ danh sách user
        Task<IEnumerable<User>> GetAllAsync();

        // Lấy 1 user theo Id
        Task<User?> GetByIdAsync(Guid id);

        // Lấy 1 user theo username
        Task<User?> GetByUsernameAsync(string username);

        // Tạo mới user
        Task<User> CreateAsync(User user);

        // Cập nhật user
        Task UpdateAsync(User user);

        // Xóa user
        Task DeleteAsync(Guid id);

        // Kiểm tra tồn tại email hoặc username
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}
