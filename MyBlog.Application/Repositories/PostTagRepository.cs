using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Entities;
using MyBlog.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly MyBlogDbContext _context;

        public PostTagRepository(MyBlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetTagsByPostIdAsync(Guid postId)
        {
            return await _context.PostTags
                .Where(pt => pt.PostId == postId)
                .Include(pt => pt.Tag)
                .Select(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTagIdAsync(Guid tagId)
        {
            return await _context.PostTags
                .Where(pt => pt.TagId == tagId)
                .Include(pt => pt.Post)
                .Select(pt => pt.Post)
                .ToListAsync();
        }

        public async Task AddTagToPostAsync(Guid postId, Guid tagId)
        {
            if (!await ExistsAsync(postId, tagId))
            {
                var postTag = new PostTag { PostId = postId, TagId = tagId };
                _context.PostTags.Add(postTag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTagFromPostAsync(Guid postId, Guid tagId)
        {
            var postTag = await _context.PostTags
                .FirstOrDefaultAsync(pt => pt.PostId == postId && pt.TagId == tagId);

            if (postTag != null)
            {
                _context.PostTags.Remove(postTag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid postId, Guid tagId)
        {
            return await _context.PostTags
                .AnyAsync(pt => pt.PostId == postId && pt.TagId == tagId);
        }
    }
}
