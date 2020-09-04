using Grocery.Microservices.BusinessLayer.Interfaces;
using Grocery.Microservices.BusinessLayer.Services.Repository;
using Grocery.Microservices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Microservices.BusinessLayer.Services
{
    public class GroceryServices : IGroceryServices
    {
        /// <summary>
        /// Creating referance Variable of IGroceryRepository and injecting in GroceryServices constructor
        /// </summary>
        private readonly IGroceryRepository _groceryRepository;
        public GroceryServices(IGroceryRepository groceryRepository)
        {
            _groceryRepository = groceryRepository;
        }
        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetAllProduct()
        {
            var result = _groceryRepository.GetAllProduct();
            return result;
        }
        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Task<Product> GetProductById(string ProductId)
        {
            var result = _groceryRepository.GetProductById(ProductId);
            return result;
        }
        /// <summary>
        /// Get acategory list
        /// </summary>
        /// <returns></returns>
        public IList<Category> CategoryList()
        {
            var result = _groceryRepository.CategoryList();
            return result;
        }
        /// <summary>
        /// Get Product by name
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public Task<IEnumerable<Product>> ProductByName(string ProductName)
        {
            var result = _groceryRepository.ProductByName(ProductName);
            return result;
        }
        /// <summary>
        /// Get Product By Category
        /// </summary>
        /// <param name="CatId"></param>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetProductByCategory(int CatId)
        {
            var result = _groceryRepository.GetProductByCategory(CatId);
            return result;
        }
        /// <summary>
        /// Place order
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Task<bool> PlaceOrder(string ProductId, string UserId)
        {
            var result = _groceryRepository.PlaceOrder(ProductId, UserId);
            return result;
        }
        /// <summary>
        /// Add new category 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<Category> AddCategory(Category category)
        {
            var result = await _groceryRepository.AddCategory(category);
            return result;
        }
        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProduct(Product product)
        {
            var result = await _groceryRepository.AddProduct(product);
            return result;
        }
        /// <summary>
        /// Get all order
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductOrder>> AllOrder()
        {
            return await _groceryRepository.AllOrder();
        }
        
        /// <summary>
        /// Get Category ById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<Category> GetCategoryById(string Id)
        {
            var result = _groceryRepository.GetCategoryById(Id);
            return result;
        }
        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<ProductOrder> GetOrderById(string OrderId)
        {
            var result = _groceryRepository.GetOrderById(OrderId);
            return result;
        }
        
        /// <summary>
        /// Remove Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveCategory(string Id)
        {
            return await _groceryRepository.RemoveCategory(Id);
        }
        /// <summary>
        /// Remove Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveProduct(string Id)
        {
            return await _groceryRepository.RemoveProduct(Id);
        }
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task<Category> UpdateCategory(string Id, Category category)
        {
            var result = _groceryRepository.UpdateCategory(Id, category);
            return result;
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task<Product> UpdateProduct(string ProductId, Product product)
        {
            var result = _groceryRepository.UpdateProduct(ProductId, product);
            return result;
        }
    }
}
