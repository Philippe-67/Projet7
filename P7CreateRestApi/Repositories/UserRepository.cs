//using P7CreateRestApi.Data;
//using P7CreateRestApi.Domain;
//using Microsoft.EntityFrameworkCore;

//namespace P7CreateRestApi.Repositories
//{
//    public class UserRepository
//    {
//        public LocalDbContext DbContext { get; }

//        public UserRepository(LocalDbContext dbContext)
//        {
//            DbContext = dbContext;
//        }

//        public User FindByUserName(string userName)
//        {
//            return DbContext.Users.Where(user => user.UserName == userName)
//                                  .FirstOrDefault();
//        }

//        public async Task<List<User>> FindAll()
//        {
//            return await DbContext.Users.ToListAsync();
//        }

//        public void Add(User user)
//        {
//        }

//        public User FindById(int id)
//        {
//            return null;
//        }
//    }
//}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
