using WhiteBoard.Shared;

namespace WhiteBoard.Models.Entities
{
    public static class EntityConverter
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto()
            {
                Id = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                NoOfPosts = user.Posts.Count
            };
        }

        public static PostDto ToDto(this Post post)
        {
            return new PostDto()
            {
                PostId = post.PostId,
                Content = post.Content,
                CreatedTime = post.CreatedTime,
                User = post.User?.ToDto()
            };
        }
    }
}