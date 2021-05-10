using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Tender.Products.Controllers;
using Tender.Products.Entities;
using Tender.Products.Repositories.Interfaces;
using Xunit;

namespace Tender.Products.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _productController;
        private readonly List<Product> _products;

        public ProductControllerTest()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _productController = new ProductController(_mockRepo.Object, _mockLogger.Object);
            _products = new List<Product>() {     new Product()
                {
                    Id = "1",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                  new Product()
                {
                    Id="2",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                } };
        }


        #region GetProducts Action


        [Fact]
        public async void GetProducts_ActionExecutes_ReturnsOkReult()
        {
            _mockRepo.Setup(p => p.GetProducts()).ReturnsAsync(_products);
            var result = await _productController.GetProducts();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetProducts_ActionExecutes_ReturnProductList()
        {
            _mockRepo.Setup(p => p.GetProducts()).ReturnsAsync(_products);
            var result = await _productController.GetProducts();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal<int>(2, returnProducts.ToList().Count);

        }


        #endregion

        #region GetProduct Action

        [Theory]
        [InlineData(null)]
        public async void GetProduct_IdIsNull_ReturnNotFound(string id)
        {
            var result = await _productController.GetProduct(id);
            var resultNotFound = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal<int>(404, resultNotFound.StatusCode);
        }

        [Theory]
        [InlineData("3")]
        public async void GetProduct_InvalidId_ReturnNotFound(string id)
        {
            Product product = null;
            _mockRepo.Setup(p => p.GetProduct(id)).ReturnsAsync(product);
            var result = await _productController.GetProduct(id);
            var resultNotFound = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal<int>(404, resultNotFound.StatusCode);
        }


        [Theory]
        [InlineData("2")]
        public async void GetProduct_ValidId_ReturnOkResult(string id)
        {
            Product product = _products.FirstOrDefault(p => p.Id == id);
            _mockRepo.Setup(p => p.GetProduct(id)).ReturnsAsync(product);
            var result = await _productController.GetProduct(id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProduct = Assert.IsAssignableFrom<Product>(okResult.Value);
            Assert.Equal(returnProduct.Id, id);
        }

        #endregion

        #region Create Action

        [Fact]
        public async void Create_ActionExecutes_ReturnCreatedAtRouteResult()
        {

            var result = await _productController.CreateProduct(_products.First());
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.IsAssignableFrom<Product>(createdResult.Value);
        }

        [Fact]
        public async void Create_InValidObjectPassed_ReturnedBadRequest()
        {
            Product product = _products.First();
            product.Name = null;
            _productController.ModelState.AddModelError("Name", "Required");
            var result = await _productController.CreateProduct(product);
            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public async void Create_InValidObjectPassed_NeverCreateExecute()
        {
            Product product = _products.First();
            product.Name = null;
            _productController.ModelState.AddModelError("Name", "Required");
            var result = await _productController.CreateProduct(product);
            _mockRepo.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Never);

        }

        [Fact]
        public async void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            Product newProduct = null;
            _mockRepo.Setup(repo => repo.Create(It.IsAny<Product>())).Callback<Product>(x => newProduct = x);
            var result = await _productController.CreateProduct(_products.First());
            _mockRepo.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Once);
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.IsAssignableFrom<Product>(createdResult.Value);
            Assert.Equal(_products.First().Name, newProduct.Name);
            
        }

        [Fact(Skip ="CreatedAtRouteResult ActionName should find")]
        public async void Create_ValidObjectPassed_TaskCompleted()
        {

            _mockRepo.Setup(repo => repo.Create(_products.First())).Returns(Task.CompletedTask);
            var result = await _productController.CreateProduct(_products.First());
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            _mockRepo.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Once);
            Assert.Equal("GetProduct", createdResult.ActionName);
            

        }


        #endregion

        #region Update Action

        [Fact]
        public async void Update_ActionExecutes_ReturnOkObjectResult()
        {

            var result = await _productController.UpdateProduct(_products.First());
            var createdResult = Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public async void Update_InValidObjectPassed_ReturnedBadRequest()
        {
            Product product = _products.First();
            product.Id = null;
            _productController.ModelState.AddModelError("Id", "Required");
            var result = await _productController.UpdateProduct(product);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async void Update_InValidObjectPassed_NeverUpdateExecute()
        {
            Product product = _products.First();
            product.Id = null;
            _productController.ModelState.AddModelError("Id", "Required");
            var result = await _productController.UpdateProduct(product);
            _mockRepo.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);

        }

        [Fact]
        public async void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            Product newProduct = null;
            _mockRepo.Setup(repo => repo.Update(It.IsAny<Product>())).Callback<Product>(x => newProduct = x);
            var result = await _productController.UpdateProduct(_products.First());
            _mockRepo.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_products.First().Id, newProduct.Id);
        }

        #endregion

        #region Delete Action

        [Fact]
        public async void Delete_ActionExecutes_ReturnOkObjectResult()
        {

            var result = await _productController.DeleteProductById(_products.First().Id);
            var createdResult = Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async void Delete_InValidObjectPassed_ReturnedBadRequest()
        {
            Product product = _products.First();
            product.Id = null;
            _productController.ModelState.AddModelError("Id", "Required");
            var result = await _productController.DeleteProductById(product.Id);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async void Delete_InValidObjectPassed_NeverDeleteExecute()
        {
            Product product = _products.First();
            product.Id = null;
            _productController.ModelState.AddModelError("Id", "Required");
            var result = await _productController.DeleteProductById(product.Id);
            _mockRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);

        }

     

        #endregion
    }
}
