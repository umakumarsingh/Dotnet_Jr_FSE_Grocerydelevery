using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Microservices.BusinessLayer.Interfaces;
using User.Microservices.BusinessLayer.ViewModels;
using User.Microservices.Entities;

namespace User.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Creating referance variable of IUserGroceryServices and IAdminGroceryServices
        /// </summary>
        private readonly IUserGroceryServices _userGS;
        //private readonly IAdminGroceryServices _adminGS;
        /// <summary>
        /// Injecting referance variable into UserController constructor
        /// </summary>
        public UserController(IUserGroceryServices userGroceryServices)
        {
            _userGS = userGroceryServices;
            //_adminGS = adminGroceryServices; //, IAdminGroceryServices adminGroceryServices
        }
        /// <summary>
        /// Get All user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> AllUser()
        {
            return await _userGS.GetAllUser();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddNewUser([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser newuser = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                MobileNumber = model.MobileNumber,
                PinCode = model.PinCode,
                HouseNo_Building_Name = model.HouseNo_Building_Name,
                Road_area = model.Road_area,
                City = model.City,
                State = model.State
            };
            await _userGS.Register(newuser);
            return Ok("User Addeed...");
        }
        /// <summary>
        /// Update registred User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Updateuser/{string UserId}")]
        public async Task<IActionResult> UpdateUser(string UserId, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getuser = _userGS.GetUserById(UserId);
            if (getuser == null)
            {
                return NotFound();
            }
            await _userGS.UpdateUser(UserId, user);
            return Ok("User Updated...");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
