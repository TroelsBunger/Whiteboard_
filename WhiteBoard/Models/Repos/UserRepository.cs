using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteBoard.Models.Entities;
using WhiteBoard.Shared;

namespace WhiteBoard.Models.Repos
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<UserDto> FindUser(int userId);
        Task<UserDto> VerifyUserLogin(string username, string password);
    }
    
    public class UserRepository : IUserRepository
    {
        private WhiteBoardContext _context;

        public UserRepository(WhiteBoardContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;
        
        public async Task<UserDto> FindUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return null;

            return user.ToDto();
        }

        public async Task<UserDto> VerifyUserLogin(string username, string password)
        {
            var user = await _context.Users.Where(u => u.UserName.Equals(username) && u.Password.Equals(password)).Include(u => u.Posts).Select(u => u.ToDto()).FirstOrDefaultAsync();

            if (user == null) return null;

            return user;
        }
    }
}