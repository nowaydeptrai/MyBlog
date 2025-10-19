using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Entities;
using MyBlog.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Application.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MyBlogDbContext _context;

        public TagRepository(MyBlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags
                .Include(t => t.PostTags)
                    .ThenInclude(pt => pt.Post)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _context.Tags
                .Include(t => t.PostTags)
                    .ThenInclude(pt => pt.Post)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> GetBySlugAsync(string slug)
        {
            return await _context.Tags
                .Include(t => t.PostTags)
                    .ThenInclude(pt => pt.Post)
                .FirstOrDefaultAsync(t => t.Slug == slug);
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Tags.AnyAsync(t => t.Name == name);
        }

        public async Task<bool> ExistsBySlugAsync(string slug)
        {
            return await _context.Tags.AnyAsync(t => t.Slug == slug);
        }
    }
}
