
using Model.Models;
using Service.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUserAscyn();

        Task<UserDTO> createUser(UserDTO user);
        Task<bool> updateUser(int id, UserDTO user);
        Task<bool> deleteUser(int id);
        Task<User> getAccountInfoByEmail(string email);
        Task<User> getAccountInfoByAccountName(string name);
        Task<User> getAccountInfoById(int id);
    }
}
