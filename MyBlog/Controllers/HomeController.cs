using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Services.Interfaces;

namespace MyBlog.Controllers
{
    [ApiController]
    [Route("api/home")] 
    public class HomeApiController : ControllerBase
    {
        private readonly IPostService _postService;

        public HomeApiController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("welcome")]
        public IActionResult GetWelcomeMessage()
        {
            var data = new
            {
                Title = "Chào mừng đến với MyBlog API",
                Message = "Trang này hiển thị bài viết mới nhất từ cơ sở dữ liệu.",
                Author = "MyBlog Team",
                Time = DateTime.Now
            };
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("posts")]
        public async Task<IActionResult> GetLatestPosts()
        {
            var posts = await _postService.GetLatestPostsAsync(5);

            var result = posts.Select(p => new
            {
                p.Title,
                p.Slug,
                p.ThumbnailUrl,
                p.CreatedAt,
                Category = p.Category?.Name ?? "Chưa phân loại",
                Author = p.Author?.FullName ?? "Ẩn danh"
            });

            return Ok(result);
        }
    }
}
