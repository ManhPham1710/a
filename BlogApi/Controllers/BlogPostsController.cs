using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        // 1. [HttpGet] GetAll()
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts); // Trả về 200 kèm danh sách BlogPost
        }

        // 2. [HttpGet("{id}")] GetById(int id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var post = await _blogPostService.GetByIdAsync(id);
                return Ok(post); // Trả về 200 kèm BlogPost
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Không tìm thấy bài viết có ID {id}" });
            }
        }

        // 3. [HttpGet("category/{categoryId}")] GetByCategory(int categoryId)
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            try
            {
                var posts = await _blogPostService.GetByCategoryAsync(categoryId);
                return Ok(posts);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Trả về 404 nếu danh mục không tồn tại
            }
        }

        // 4. [HttpPost] Create([FromBody] BlogPost blogPost)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogPost blogPost)
        {
            try
            {
                var createdPost = await _blogPostService.CreateAsync(blogPost);
                // Trả về 201 Created cùng đường dẫn tới bài viết mới
                return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, createdPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // Lỗi logic/dữ liệu đầu vào
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Lỗi không tìm thấy CategoryId
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi hệ thống xảy ra: " + ex.Message });
            }
        }

        // 5. [HttpPut("{id}")] Update(int id, [FromBody] BlogPost blogPost)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogPost blogPost)
        {
            try
            {
                var updatedPost = await _blogPostService.UpdateAsync(id, blogPost);
                return Ok(updatedPost); // Trả về 200 kèm dữ liệu đã cập nhật
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật bài viết: " + ex.Message });
            }
        }

        // 6. [HttpDelete("{id}")] Delete(int id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _blogPostService.DeleteAsync(id);
                return Ok(new { message = "Xóa bài viết thành công" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa bài viết: " + ex.Message });
            }
        }
    }
}