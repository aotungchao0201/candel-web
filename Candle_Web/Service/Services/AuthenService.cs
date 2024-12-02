using AutoMapper;
using Repo.Repository.Interface;
using Service.Modals.Request;
using Service.Modals.Respond;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public AuthenService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<LoginRequest> Login(string email, string password)
        {
            var user = await _userRepo.GetUserByGmail(email);
            if (user == null || user.PasswordHash != password)
            {
                return null;
            }

            var response = _mapper.Map<LoginRequest>(user);

            return response;
        }
    }
}
