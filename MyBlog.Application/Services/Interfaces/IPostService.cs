using MyBlog.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Application.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetLatestPostsAsync(int count = 5);
        Task<Post?> GetPostBySlugAsync(string slug);
    }
}
