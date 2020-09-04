using Microsoft.AspNetCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.BusinessLayer.Interfaces;
using User.Microservices.BusinessLayer.Services.Repository;
using User.Microservices.Entities;

namespace User.Microservices.BusinessLayer.Services
{
    public class UserGroceryServices : IUserGroceryServices
    {
        /// <summary>
        /// Creating referance Variable of IUserGroceryRepository and injecting in UserGroceryServices constructor
        /// </summary>
        private readonly IUserGroceryRepository _userGroceryRepository;
        public UserGroceryServices(IUserGroceryRepository userGroceryRepository)
        {
            _userGroceryRepository = userGroceryRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            var alluser = await _userGroceryRepository.GetAllUser();
            return alluser;
        }

        public async Task<ApplicationUser> GetUserById(string UserId)
        {
            var result = await _userGroceryRepository.GetUserById(UserId);
            return result;
        }

        public async Task<ApplicationUser> Login(string Email, string password)
        {
            var result = await _userGroceryRepository.Login(Email, password);
            return result;
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> Register(ApplicationUser user)
        {
            var result = await _userGroceryRepository.Register(user);
            return result;
        }

        public async Task<ApplicationUser> UpdateUser(string UserId, ApplicationUser user)
        {
            var update = await _userGroceryRepository.UpdateUser(UserId, user);
            return update;
        }
    }
}
