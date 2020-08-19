using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiteBoard.Models;
using WhiteBoard.Models.Entities;
using WhiteBoard.Models.Repos;
using WhiteBoard.Shared;

namespace WhiteBoard.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> Get()
        {
            var postList = await _postRepository.GetAllPosts();

            if (postList == null) return NoContent();

            return Ok(postList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            var post = await _postRepository.GetPostById(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByUserId(int id)
        {
            var postList = await _postRepository.GetPostByUserId(id);
            if (postList == null) return NoContent();

            return Ok(postList);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Post([FromBody] PostDto post)
        {
            var result = await _postRepository.CreatePost(post);

            if (result == null) return BadRequest();
            
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<PostDto>> Delete(int id)
        {
            var result = await _postRepository.DeletePost(id);
            if (result == null) return NotFound();
            return NoContent();
        }
    }
}