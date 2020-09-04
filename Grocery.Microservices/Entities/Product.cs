using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Microservices.Entities
{
    public class Product
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public Double Amount { get; set; }
        [Required]
        [Display(Name = "Stock")]
        public int stock { get; set; }
        public string photo { get; set; }
        public int CatId { get; set; }
        public ProductOrder ProductOrder { get; set; }
    }
}
