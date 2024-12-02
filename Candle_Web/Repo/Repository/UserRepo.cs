using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly candleContext _context;

        public UserRepo(candleContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUser()
        {
            var data = await _context.Users.ToListAsync();
            return data;
        }

        public async Task<User> GetUserByGmail(string gmail)
        {
            var data = await _context.Users.SingleOrDefaultAsync(x => x.Email.Equals(gmail));
            return data;
        }

        public async Task<User> GetUserById(int id)
        {
            var data = await _context.Users.SingleOrDefaultAsync(x => x.UserId.Equals(id));
            return data;
        }

        public async Task<User> GetUserByName(string name)
        {
            var data = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(name));
            return data;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
