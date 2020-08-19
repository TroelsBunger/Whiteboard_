using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhiteBoard.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

       
    }
}