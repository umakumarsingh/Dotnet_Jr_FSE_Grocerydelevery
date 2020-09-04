using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.DataLayer;
using User.Microservices.Entities;

namespace User.Microservices.BusinessLayer.Services.Repository
{
    public class UserGroceryRepository : IUserGroceryRepository
    {
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<ApplicationUser> _dbCollection;
        public UserGroceryRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<ApplicationUser>(typeof(ApplicationUser).Name);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            try
            {
                var list = _mongoContext.GetCollection<ApplicationUser>("ApplicationUser")
                .Find(Builders<ApplicationUser>.Filter.Empty, null)
                .SortByDescending(e => e.UserId);
                return await list.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ApplicationUser> GetUserById(string UserId)
        {
            try
            {
                var objectId = new ObjectId(UserId);
                FilterDefinition<ApplicationUser> filter = Builders<ApplicationUser>.Filter.Eq("UserId", objectId);
                _dbCollection = _mongoContext.GetCollection<ApplicationUser>(typeof(ApplicationUser).Name);
                return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<ApplicationUser> Login(string Email, string password)
        {
            try
            {
                var email = Builders<ApplicationUser>.Filter.Eq("Email", Email);
                var pass = Builders<ApplicationUser>.Filter.Eq("Password", password);
                var filterCriteria = Builders<ApplicationUser>.Filter.And(email, pass);
                var userFind = await _dbCollection.FindAsync(filterCriteria);
                return userFind.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> Register(ApplicationUser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(typeof(ApplicationUser).Name + "Object is Null");
                }
                _dbCollection = _mongoContext.GetCollection<ApplicationUser>(typeof(ApplicationUser).Name);
                await _dbCollection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return user;
        }

        public async Task<ApplicationUser> UpdateUser(string UserId, ApplicationUser user)
        {
            if (user == null && UserId == null)
            {
                throw new ArgumentNullException(typeof(ApplicationUser).Name + "Object or may be UserId is Null");
            }
            var update = await _dbCollection.FindOneAndUpdateAsync(Builders<ApplicationUser>.
                Filter.Eq("UserId", user.UserId), Builders<ApplicationUser>.
                Update.Set("Name", user.Name).Set("Email", user.Email).Set("Password", user.Password)
                .Set("MobileNumber", user.MobileNumber).Set("PinCode", user.PinCode).Set("HouseNo_Building_Name", user.HouseNo_Building_Name)
                .Set("Road_area", user.Road_area).Set("City", user.City).Set("State", user.State));
            return update;
        }
    }
}
