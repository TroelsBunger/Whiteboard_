using System;

namespace WhiteBoard.Models.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Post()
        {
            CreatedTime = DateTime.Now;
        }
    }
    
    
}