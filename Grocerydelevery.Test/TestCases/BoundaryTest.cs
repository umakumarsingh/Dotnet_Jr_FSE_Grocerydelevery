using Grocery.Microservices.BusinessLayer.Interfaces;
using Grocery.Microservices.BusinessLayer.Services;
using Grocery.Microservices.BusinessLayer.Services.Repository;
using Grocery.Microservices.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using User.Microservices.BusinessLayer.Interfaces;
using User.Microservices.BusinessLayer.Services;
using User.Microservices.BusinessLayer.Services.Repository;
using User.Microservices.Entities;
using Xunit;

namespace Grocerydelevery.Test.TestCases
{
    public class BoundaryTest
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
        public BoundaryTest()
        {
            /// <summary>
            /// Injecting service object into Test class constructor
            /// </summary>
            _groceryS = new GroceryServices(groceryservice.Object);
            _userGroceryS = new UserGroceryServices(userservice.Object);
            _user = new ApplicationUser()
            {
                UserId = "5f45df48ff7f1df2085ec8fd",
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
        static BoundaryTest()
        {
            if (!File.Exists("../../../../output_boundary_revised.txt"))
                try
                {
                    File.Create("../../../../output_boundary_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_boundary_revised.txt");
                File.Create("../../../../output_boundary_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// Testfor_ValidateUserId is used to test register user is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateUserId()
        {
            //Arrange
            bool res = false;
            //Act
            userservice.Setup(repo => repo.Register(_user)).ReturnsAsync(_user);
            var result = await _userGroceryS.Register(_user);

            if (result.UserId.Length.ToString() == _user.UserId.Length.ToString())
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateUserId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Testfor_ValidateProductId is used to test ProductId is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateProductId()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.AddProduct(_product)).ReturnsAsync(_product);
            var result = await _groceryS.AddProduct(_product);

            if (result.ProductId.Length.ToString() == _product.ProductId.Length.ToString())
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateProductId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Testfor_ValidateCategoryId is used for test the CategoryId is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateCategoryId()
        {
            //Arrange
            bool res = false;
            //Act
            groceryservice.Setup(repo => repo.AddCategory(_category)).ReturnsAsync(_category);
            var result = await _groceryS.AddCategory(_category);

            if (result.Id.Length.ToString() == _category.Id.Length.ToString())
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateCategoryId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Testfor_ValidEmail to test email id is valid or not
        /// </summary>
        [Fact]
        public async void Testfor_ValidEmail()
        {
            //Arrange
            bool res = false;
            //Act
            bool isEmail = Regex.IsMatch(_user.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            //Assert
            Assert.True(isEmail);
            res = isEmail;
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidEmail=" + res + "\n");
        }
        /// <summary>
        /// Testfor_ValidateMobileNumber is used for test mobile number is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateMobileNumber()
        {
            //Arrange
            bool res = false;
            //Act
            userservice.Setup(repo => repo.Register(_user)).ReturnsAsync(_user);
            var result = await _userGroceryS.Register(_user);
            var actualLength = _user.MobileNumber.ToString().Length;
            if (result.MobileNumber.ToString().Length == actualLength)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_boundary_revised.txt", "Testfor_ValidateMobileNumber=" + res + "\n");
            return res;
        }
    }
}
