using Grocery.Microservices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Microservices.BusinessLayer.Services.Repository
{
    public interface IGroceryRepository
    {
        //List of method to perform all related operation
        Task<bool> PlaceOrder(string ProductId, string UserId);
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(string ProductId);
        Task<IEnumerable<Product>> GetProductByCategory(int CatId);
        Task<IEnumerable<Product>> ProductByName(string ProductName);
        IList<Category> CategoryList();
        Task<Category> AddCategory(Category category);
        Task<bool> RemoveCategory(string Id);
        Task<Category> UpdateCategory(string Id, Category category);
        Task<Product> AddProduct(Product product);
        Task<bool> RemoveProduct(string ProductId);
        Task<Product> UpdateProduct(string ProductId, Product product);
        Task<Category> GetCategoryById(string Id);
        Task<IEnumerable<ProductOrder>> AllOrder();
        Task<ProductOrder> GetOrderById(string OrderId);
    }
}
