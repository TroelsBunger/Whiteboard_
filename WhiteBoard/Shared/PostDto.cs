using System;

namespace WhiteBoard.Shared
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public UserDto User { get; set; }
    }
}