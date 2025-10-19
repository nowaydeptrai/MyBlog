using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Entities;
using MyBlog.Application.Services.Interfaces;

namespace MyBlog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly MyBlogDbContext _db;

        public PostService(MyBlogDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Post>> GetLatestPostsAsync(int count = 5)
        {
            return await _db.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Post?> GetPostBySlugAsync(string slug)
        {
            return await _db.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
