using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Microservices.Entities
{
    public class Category
    { 
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CatId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public bool OpenInNewWindow { get; set; }

    }
}
