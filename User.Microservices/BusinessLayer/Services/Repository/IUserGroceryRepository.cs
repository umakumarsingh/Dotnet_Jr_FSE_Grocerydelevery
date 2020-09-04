using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.Entities;

namespace User.Microservices.BusinessLayer.Services.Repository
{
    public interface IUserGroceryRepository
    {
        Task<ApplicationUser> Register(ApplicationUser user);
        Task<ApplicationUser> GetUserById(string UserId);
        Task<IEnumerable<ApplicationUser>> GetAllUser();
        Task<ApplicationUser> UpdateUser(string UserId, ApplicationUser user);
        Task<ApplicationUser> Login(string Email, string password);
        Task<bool> Logout();
    }
}
