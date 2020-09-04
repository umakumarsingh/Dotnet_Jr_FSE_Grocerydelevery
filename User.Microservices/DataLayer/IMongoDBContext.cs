using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Microservices.DataLayer
{
    public interface IMongoDBContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
