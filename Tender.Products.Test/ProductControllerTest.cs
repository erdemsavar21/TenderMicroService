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
            _productController = new ProductController(_mockRepo.Object,_mockLogger.Object);
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
        public async void GetProduct_ProductIsNotFound_ReturnNotFound(string id)
        {
            Product product = null;
            _mockRepo.Setup(p => p.GetProduct(id)).ReturnsAsync(product);
            var result = await _productController.GetProduct(id);
            var resultNotFound = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal<int>(404, resultNotFound.StatusCode);
        }
    }
}
