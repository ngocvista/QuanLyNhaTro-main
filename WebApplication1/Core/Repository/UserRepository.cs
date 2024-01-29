using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Core.IRepository;
using MotelManagement.Data.Models;

namespace MotelManagement.Core.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MotelManagementContext context) : base(context)
        {
        }

        public void Add(User entity)
        {
            _context.Add(entity);
        }

        public async Task<User> Authentication(string username, string password)
        {
            User user = await _context.Users.Where(user => user.Password.Equals(password)
                                &&user.Email.Equals(username)).FirstOrDefaultAsync();
            return user;

        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {

        }

        public void ChangePassword(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).Property(x => x.Password).IsModified = true;      
        }
        public void ChangeInfo(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).Property(x => x.FullName).IsModified = true;
            _context.Entry(user).Property(x => x.Email).IsModified = true;
            _context.Entry(user).Property(x => x.Gender).IsModified = true;
            _context.Entry(user).Property(x => x.Phone).IsModified = true;
            _context.Entry(user).Property(x => x.Address).IsModified = true;
            _context.Entry(user).Property(x => x.Dob).IsModified = true;
        }
    }
}
