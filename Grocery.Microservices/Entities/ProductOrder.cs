using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Microservices.Entities
{
    public class ProductOrder
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
