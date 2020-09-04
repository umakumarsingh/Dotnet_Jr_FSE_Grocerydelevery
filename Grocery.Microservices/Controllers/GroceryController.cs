using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grocery.Microservices.BusinessLayer.Interfaces;
using Grocery.Microservices.BusinessLayer.ViewModels;
using Grocery.Microservices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grocery.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        /// <summary>
        /// Creating referance variable of IGroceryServices and IUserGroceryServices
        /// </summary>
        private readonly IGroceryServices _groceryServices;

        //private readonly IUserGroceryServices _userGroceryServices;
        /// <summary>
        /// Injecting referance variable into GroceryController constructor
        /// </summary>
        public GroceryController(IGroceryServices groceryServices)
        {
            _groceryServices = groceryServices;
            // _userGroceryServices = userGroceryServices; , IUserGroceryServices userGroceryServices
        }
        /// <summary>
        /// Get All product and show Index Page for user
        /// </summary>
        /// <returns></returns>
        // GET: api/<GroceryController>
        [HttpGet]
        public async Task<IEnumerable<Product>> AllProduct()
        {
            return await _groceryServices.GetAllProduct();
        }
        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ProductById/{ProductId}")]
        public async Task<IActionResult> ProductById(string ProductId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getproduct = await _groceryServices.GetProductById(ProductId);
            if (getproduct == null)
            {
                return NotFound();
            }
            return CreatedAtAction("AllProduct", new { ProductId = getproduct.ProductId }, getproduct);
        }
        /// <summary>
        /// Get All product by CategoryId
        /// </summary>
        /// <param name="CatId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ProductByCategory/{CatId}")]
        public async Task<IEnumerable<Product>> ProductByCategory(int CatId)
        {
            return await _groceryServices.GetProductByCategory(CatId);
        }
        /// <summary>
        /// Get product by product Name
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ProductByName/{ProductName}")]
        public async Task<IEnumerable<Product>> ProductByName(string ProductName)
        {
            return await _groceryServices.ProductByName(ProductName);
        }
        /// <summary>
        /// Get all category list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Categorylist")]
        public IActionResult GetCategoryList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getcategory = _groceryServices.CategoryList();
            if (getcategory == null)
            {
                return NotFound();
            }
            return CreatedAtAction("AllProduct", getcategory);
        }
        /// <summary>
        /// Place order for Registred user
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("Placeorder/{ProductId}/{email}/{password}")]
        //public async Task<IActionResult> Placeorder(string ProductId, string email, string password)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    var result = await _userGroceryServices.Login(email, password);
        //    if (result != null)
        //    {
        //        await _groceryServices.PlaceOrder(ProductId, result.UserId);
        //    }
        //    return Ok("Order Placed...");
        //}
        /// <summary>
        /// Get All order plced by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allorder")]
        public async Task<IEnumerable<ProductOrder>> AllOrder()
        {
            return await _groceryServices.AllOrder();
        }
        /// <summary>
        /// Get product Order By Id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderById/{OrderId}")]
        public async Task<IActionResult> OrderById(string OrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getorder = await _groceryServices.GetOrderById(OrderId);
            if (getorder == null)
            {
                return NotFound();
            }
            return CreatedAtAction("AllOrder", new { OrderId = getorder.OrderId }, getorder);
        }
        /// <summary>
        /// Add new category in MongoDb Collection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category newcategory = new Category
            {
                Title = model.Title,
                Url = model.Url,
                OpenInNewWindow = model.OpenInNewWindow
            };
            await _groceryServices.AddCategory(newcategory);
            return Ok("New Category Addeed...");
        }
        /// <summary>
        /// Add new Product in MongoDb Collection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product newproduct = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Amount = model.Amount,
                stock = model.stock,
                photo = model.photo,
                CatId = model.CatId
            };
            await _groceryServices.AddProduct(newproduct);
            return Ok("Product Addeed...");
        }
        /// <summary>
        /// Updatecategory in MongoDb Collection
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Updatecategory/{Id}")]
        public async Task<IActionResult> Updatecategory(string Id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getcategory = _groceryServices.GetCategoryById(Id);
            if (getcategory == null)
            {
                return NotFound();
            }
            await _groceryServices.UpdateCategory(Id, category);
            return Ok("Category Updated...");
        }
        /// <summary>
        /// Update Product in MongoDb
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProduct/{ProductId}")]
        public async Task<IActionResult> UpdateProduct(string ProductId, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var getproduct = _groceryServices.GetProductById(ProductId);
            if (getproduct == null)
            {
                return NotFound();
            }
            await _groceryServices.UpdateProduct(ProductId, product);
            return Ok("Product Updated...");
        }
        /// <summary>
        /// Remove Category form MmongoDb by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Removecategory/{Id}")]
        public async Task<IActionResult> RemoveCategory(string Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _groceryServices.RemoveCategory(Id);
                if (result == false)
                {
                    return NotFound();
                }
                return Ok("Category Deleted");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Remove Product from MongoDb
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Removeproduct/{ProductId}")]
        public async Task<IActionResult> RemoveProduct(string ProductId)
        {
            if (ProductId == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _groceryServices.RemoveProduct(ProductId);
                if (result == false)
                {
                    return NotFound();
                }
                return Ok("Product Deleted");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
