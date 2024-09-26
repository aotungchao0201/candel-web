using AutoMapper;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Modals;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserDTO> createUser(UserDTO user)
        {
            try
            {
                var map = _mapper.Map<User>(user);
                var userCreate = await _userRepo.CreateUser(map);
                var resutl = _mapper.Map<UserDTO>(userCreate);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deleteUser(int id)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    throw new Exception($"User {id} does not exist");
                }

                await _userRepo.DeleteUser(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> getAccountInfoByAccountName(string name)
        {
            var data = await _userRepo.GetUserByName(name);
            return data;
        }

        public async Task<User> getAccountInfoByEmail(string email)
        {
            var data = await _userRepo.GetUserByGmail(email);
            return data;
        }

        public async Task<User> getAccountInfoById(int id)
        {
            var data = await _userRepo.GetUserById(id);
            return data;
        }

        public async Task<List<UserDTO>> GetAllUserAscyn()
        {
            try
            {

                var data = await _userRepo.GetAllUser();
                var map = _mapper.Map<List<UserDTO>>(data);
                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> updateUser(int id, UserDTO user)
        {
            try
            {
                var userData = await _userRepo.GetUserById(id);
                if (userData == null)
                {
                    return false;
                }

                _mapper.Map(user, userData);
                await _userRepo.UpdateUser(userData);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Fail to update user info {ex.Message}");
                return false;
            }
        }

       
    }
}
