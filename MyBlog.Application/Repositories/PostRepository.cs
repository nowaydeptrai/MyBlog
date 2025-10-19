using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Entities;
using MyBlog.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyBlogDbContext _context;

        public PostRepository(MyBlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(bool includeRelations = false)
        {
            IQueryable<Post> query = _context.Posts;

            if (includeRelations)
            {
                query = query
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .Include(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag);
            }

            return await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(Guid id, bool includeRelations = false)
        {
            IQueryable<Post> query = _context.Posts;

            if (includeRelations)
            {
                query = query
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .Include(p => p.Comments)
                    .Include(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> GetBySlugAsync(string slug, bool includeRelations = false)
        {
            IQueryable<Post> query = _context.Posts;

            if (includeRelations)
            {
                query = query
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .Include(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag);
            }

            return await query.FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<IEnumerable<Post>> GetByCategoryAsync(Guid categoryId)
        {
            return await _context.Posts
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByAuthorAsync(Guid authorId)
        {
            return await _context.Posts
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Post> CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsBySlugAsync(string slug)
        {
            return await _context.Posts.AnyAsync(p => p.Slug == slug);
        }
    }
}
