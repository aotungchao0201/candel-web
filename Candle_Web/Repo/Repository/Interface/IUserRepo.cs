using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository.Interface
{
    public interface IUserRepo
    {
        public Task<List<User>> GetAllUser();
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<bool> DeleteUser(User user);

        public Task<User> GetUserById(int id);
        public Task<User> GetUserByName(string name);
        public Task<User> GetUserByGmail(string gmail);

    }
}
