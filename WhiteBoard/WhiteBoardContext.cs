using Microsoft.EntityFrameworkCore;
using WhiteBoard.Models;
using WhiteBoard.Models.Entities;

namespace WhiteBoard
{
    public class WhiteBoardContext : DbContext
    {
        public WhiteBoardContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}