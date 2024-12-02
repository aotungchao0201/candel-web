using AutoMapper;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Modals;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
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

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            try
            {
                // Map UserDTO to User entity
                var userEntity = _mapper.Map<User>(user);

                // Call repository to create user
                var userCreate = await _userRepo.CreateUser(userEntity);

                // Map created User entity back to UserDTO
                var result = _mapper.Map<UserDTO>(userCreate);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow as a custom exception
                throw new UserServiceException("An error occurred while creating user.", ex);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                // Get user by ID
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    throw new UserServiceException($"User with ID {id} does not exist.");
                }

                // Delete user from repository
                await _userRepo.DeleteUser(user);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow as a custom exception
                throw new UserServiceException("An error occurred while deleting user.", ex);
            }
        }

        public async Task<User> GetAccountInfoByAccountName(string name)
        {
            try
            {
                var data = await _userRepo.GetUserByName(name);
                if (data == null)
                {
                    throw new UserServiceException($"User with name {name} not found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new UserServiceException("An error occurred while fetching account by name.", ex);
            }
        }

        public async Task<User> GetAccountInfoByEmail(string email)
        {
            try
            {
                var data = await _userRepo.GetUserByGmail(email);
                if (data == null)
                {
                    throw new UserServiceException($"User with email {email} not found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new UserServiceException("An error occurred while fetching account by email.", ex);
            }
        }

        public async Task<User> GetAccountInfoById(int id)
        {
            try
            {
                var data = await _userRepo.GetUserById(id);
                if (data == null)
                {
                    throw new UserServiceException($"User with ID {id} not found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new UserServiceException("An error occurred while fetching account by ID.", ex);
            }
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepo.GetAllUser();
                var userDTOs = _mapper.Map<List<UserDTO>>(users);
                return userDTOs;
            }
            catch (Exception ex)
            {
                throw new UserServiceException("An error occurred while fetching all users.", ex);
            }
        }

        public async Task<bool> UpdateUser(int id, UserDTO user)
        {
            try
            {
                // Get existing user by ID
                var userData = await _userRepo.GetUserById(id);
                if (userData == null)
                {
                    throw new UserServiceException($"User with ID {id} not found.");
                }

                // Map DTO to user entity
                _mapper.Map(user, userData);

                // Update user in repository
                await _userRepo.UpdateUser(userData);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow as a custom exception
                throw new UserServiceException("An error occurred while updating user.", ex);
            }
        }
    }

    // Custom exception to handle errors in UserService
    public class UserServiceException : Exception
    {
        public UserServiceException(string message) : base(message)
        {
        }

        public UserServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
