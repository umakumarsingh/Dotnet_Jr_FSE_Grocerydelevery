using Grocery.Microservices.BusinessLayer.Interfaces;
using Grocery.Microservices.BusinessLayer.Services;
using Grocery.Microservices.BusinessLayer.Services.Repository;
using Grocery.Microservices.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using User.Microservices.BusinessLayer.Interfaces;
using User.Microservices.BusinessLayer.Services;
using User.Microservices.BusinessLayer.Services.Repository;
using User.Microservices.Entities;
using Xunit;

namespace Grocerydelevery.Test.TestCases
{
    public class FuctionalTests
    {
        /// <summary>
        /// Creating Referance Variable and Mocking repository class
        /// </summary>
        private readonly IGroceryServices _groceryS;
        private readonly IUserGroceryServices _userGroceryS;
        public readonly Mock<IGroceryRepository> groceryservice = new Mock<IGroceryRepository>();
        public readonly Mock<IUserGroceryRepository> userservice = new Mock<IUserGroceryRepository>();
        private ApplicationUser _user;
        private Product _product;
        private Category _category;
        private ProductOrder _productOrder;
        public FuctionalTests()
        {
            /// <summary>
            /// Injecting service object into Test class constructor
            /// </summary>
            _groceryS = new GroceryServices(groceryservice.Object);
            _userGroceryS = new UserGroceryServices(userservice.Object);
            _user = new ApplicationUser()
            {
                Name = "Uma Kumar",
                Email = "umakumarsingh@gmail.com",
                Password = "12345",
                MobileNumber = 9865253568,
                PinCode = 820003,
                HouseNo_Building_Name = "9/11",
                Road_area = "Road_area",
                City = "Gaya",
                State = "Bihar"
            };
            _product = new Product()
            {
                ProductId = "5f45df48ff7f1df2085ec8fd",
                ProductName = "Samsung",
                Description = "Procesor i9, 2 GB, 32 GB SSD, Corning Grollia Glass",
                Amount = 24900.0,
                stock = 10,
                photo = "",
                CatId = 1
            };
            _category = new Category()
            {
                Id = "5f0ff60a7b7be11c4c3c19e1",
                CatId = 1,
                Url = "~/Home",
                OpenInNewWindow = false
            };
            _productOrder = new ProductOrder()
            {
                OrderId = "5f48bf68c558892d2e17a70c",
                ProductId = "5f45df48ff7f1df2085ec8fd",
                UserId = "5f4f6639016adae865d627ff"
            };
        }
        /// <summary>
        /// Creating test output text file that store the result in boolean result
        /// </summary>
        static FuctionalTests()
        {
            if (!File.Exists("../../../../output_revised.txt"))
                try
                {
                    File.Create("../../../../output_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_revised.txt");
                File.Create("../../../../output_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// Test to validate user is valid for register
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_ValidUserRegister()
        {
            //Arrange
            bool res = false;
            //Act
            userservice.Setup(repo => repo.Register(_user)).ReturnsAsync(_user);
            var result = await _userGroceryS.Register(_user);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_ValidUserRegister=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test for get a valid user id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetUserById()
        {
            //Arrange
            bool res = false;
            //Act
            userservice.Setup(repo => repo.GetUserById(_user.UserId)).ReturnsAsync(_user);
            var result = await _userGroceryS.GetUserById(_user.UserId);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetUserById=" + res + "\n");
            return res;
        }
        [Fact]
        public async Task<bool> Testfor_Validate_GetLoginUser()
        {
            //Arrange
            bool res = false;
            //Act
            userservice.Setup(repo => repo.Login(_user.Email, _user.Password)).ReturnsAsync(_user);
            var result = await _userGroceryS.Login(_user.Email, _user.Password);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetLoginUser=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate user update is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateUser()
        {
            //Arrange
            bool res = false;
            var _userUpdate = new ApplicationUser()
            {
                UserId = "5f0ff60a7b7be11c4c3c19e1",
                Name = "Uma Kumar",
                Email = "umakumarsingh@gmail.com",
                Password = "12345",
                MobileNumber = 9865253568,
                PinCode = 820003,
                HouseNo_Building_Name = "9/11",
                Road_area = "Road_area",
                City = "Gaya",
                State = "Bihar"
            };
            //Act
            userservice.Setup(repo => repo.UpdateUser(_userUpdate.UserId, _userUpdate)).ReturnsAsync(_userUpdate);
            var result = await _userGroceryS.UpdateUser(_userUpdate.UserId, _userUpdate);
            if (result == _userUpdate)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateUser=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate all product is listing or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetAllProduct()
        {
            //Arrange
            var res = false;
            //Action
            groceryservice.Setup(repos => repos.GetAllProduct());
            var result = await _groceryS.GetAllProduct();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetAllProduct=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate get product by id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetProductById()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.GetProductById(_product.ProductId)).ReturnsAsync(_product);
            var result = await _groceryS.GetProductById(_product.ProductId);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetProductById=" + res + "\n");
            return res;
        }
        /// <summary>
        /// test to validate get category by category id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetCategoryById()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.GetCategoryById(_category.Id)).ReturnsAsync(_category);
            var result = await _groceryS.GetCategoryById(_category.Id);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetCategoryById=" + res + "\n");
            return res;
        }
        /// <summary>
        /// test to validate get product by category
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetProductByCategory()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.GetProductByCategory(_product.CatId));
            var result = await _groceryS.GetProductByCategory(_product.CatId);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetProductByCategory=" + res + "\n");
            return res;
        }
        /// <summary>
        /// test to validate get product by product name.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetProductByName()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.ProductByName(_product.ProductName));
            var result = await _groceryS.ProductByName(_product.ProductName);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetProductByName=" + res + "\n");
            return res;
        }
        /// <summary>
        /// test to validate get category list
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetCategorylist()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.CategoryList());
            var result = _groceryS.CategoryList();
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetCategorylist=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate valid category is added or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_ValidAddCategory()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.AddCategory(_category)).ReturnsAsync(_category);
            var result = await _groceryS.AddCategory(_category);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_ValidAddCategory=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate valid product is added or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_AddValidProduct()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.AddProduct(_product)).ReturnsAsync(_product);
            var result = await _groceryS.AddProduct(_product);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_AddValidProduct=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate valid categroy is updated or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateCategory()
        {
            //Arrange
            bool res = false;
            var _categoryUpdate = new Category()
            {
                Id = "5f0ff60a7b7be11c4c3c19e1",
                CatId = 1,
                Url = "~/Home",
                OpenInNewWindow = false
            };
            //Act
            groceryservice.Setup(repo => repo.UpdateCategory(_categoryUpdate.Id, _categoryUpdate)).ReturnsAsync(_categoryUpdate);
            var result = await _groceryS.UpdateCategory(_categoryUpdate.Id, _categoryUpdate);
            if (result == _categoryUpdate)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateCategory=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate update valid product 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateProduct()
        {
            //Arrange
            bool res = false;
            var _updateproduct = new Product()
            {
                ProductId = "5f45df48ff7f1df2085ec8fd",
                ProductName = "Samsung",
                Description = "Procesor i9, 2 GB, 32 GB SSD, Corning Grollia Glass",
                Amount = 24900.0,
                stock = 10,
                photo = "",
                CatId = 1
            };
            //Act
            groceryservice.Setup(repo => repo.UpdateProduct(_updateproduct.ProductId, _updateproduct)).ReturnsAsync(_updateproduct);
            var result = await _groceryS.UpdateProduct(_updateproduct.ProductId, _updateproduct);
            if (result == _updateproduct)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateProduct=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate valid category is removed or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_RemoveCategory()
        {
            //Arrange
            var res = false;
            //Action
            groceryservice.Setup(repos => repos.RemoveCategory(_category.Id)).ReturnsAsync(true);
            var resultDelete = await _groceryS.RemoveCategory(_category.Id);
            //Assertion
            if (resultDelete == true)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_RemoveCategory=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate Valid product is removed or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_RemoveProduct()
        {
            //Arrange
            var res = false;
            //Action
            groceryservice.Setup(repos => repos.RemoveProduct(_product.ProductId)).ReturnsAsync(true);
            var resultDelete = await _groceryS.RemoveProduct(_product.ProductId);
            //Assertion
            if (resultDelete == true)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_RemoveProduct=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate get all product
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetAllOrder()
        {
            //Arrange
            var res = false;
            //Action
            groceryservice.Setup(repos => repos.AllOrder());
            var result = await _groceryS.AllOrder();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetAllOrder=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Get Order by order id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_GetOrderById()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.GetOrderById(_productOrder.OrderId)).ReturnsAsync(_productOrder);
            var result = await _groceryS.GetOrderById(_productOrder.OrderId);
            if (result != null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_GetOrderById=" + res + "\n");
            return res;
        }
    }
}
