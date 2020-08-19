using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteBoard.Models.Entities;
using WhiteBoard.Shared;

namespace WhiteBoard.Models.Repos
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        Task<IEnumerable<PostDto>> GetAllPosts();
        Task<PostDto> GetPostById(int id);
        Task<IEnumerable<PostDto>> GetPostByUserId(int userId);
        Task<PostDto> CreatePost(PostDto postDto);
        Task<PostDto> DeletePost(int postId);

    }
    
    public class PostRepository : IPostRepository
    {
        private WhiteBoardContext _context;

        public PostRepository(WhiteBoardContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public async Task<IEnumerable<PostDto>> GetAllPosts()
        {
            return await Posts.OrderByDescending(p => p.CreatedTime).Include(p => p.User).Select(p => p.ToDto()).ToListAsync();
        }

        public async Task<PostDto> GetPostById(int id)
        {
            var post = Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.PostId == id).Result;

            if (post == null) return null;

            return post.ToDto();
        }

        public async Task<IEnumerable<PostDto>> GetPostByUserId(int userID)
        {
            return await Posts.Where(p => p.UserId == userID).OrderByDescending(p => p.CreatedTime)
                .Select(p => p.ToDto()).ToListAsync();
            
        }

        public async Task<PostDto> CreatePost(PostDto postDto)
        {
            if (postDto.Content == null || postDto.User.Id == null) return null;
            
            var post = new Post()
            {
                Content = postDto.Content,
                UserId = postDto.User.Id ?? 0,
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return postDto;
        }

        public async Task<PostDto> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null) return null;

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

            return post.ToDto();
        }
    }
}