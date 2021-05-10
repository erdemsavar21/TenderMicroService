using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Tender.Products.Data;
using Tender.Products.Data.Interfaces;
using Tender.Products.Entities;
using Tender.Products.Repositories;
using Tender.Products.Settings;
using Xunit;
using System.Linq;
using System.Threading;

namespace Tender.Products.Test
{
    public class ProductContextTest
    {
        private ProductDatabaseSettings _productDatabaseSettings;
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;
        private Mock<IProductDatabaseSettings> _mockOptions;
        private Product _product;
        private List<Product> _list;

        public ProductContextTest()
        {
            _product = new Product()
            {
                Name = "IPhone X",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            };

            _productDatabaseSettings = new ProductDatabaseSettings() { ConnectionString = "mongodb://localhost:27017", DatabaseName = "TenderingMongoDb", CollectionName = "Products" };

           
            _list = new List<Product>();
            _list.Add(_product);

            _mockOptions = new Mock<IProductDatabaseSettings>();

            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
            
            

           
        }

        [Fact]
        public void MongoDBContext_Constructor_Success()
        {

            _mockOptions.Setup(c => c.CollectionName).Returns(_productDatabaseSettings.CollectionName);
            _mockOptions.Setup(c => c.DatabaseName).Returns(_productDatabaseSettings.DatabaseName);
            _mockOptions.Setup(c => c.ConnectionString).Returns(_productDatabaseSettings.ConnectionString);
            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.DatabaseName, null)).Returns(_mockDB.Object);

            var context = new ProductContext(_mockOptions.Object);

            Assert.NotNull(context);
        }

        [Fact]
        public void MongoDBContext_GetCollection_ValidName_Success()
        {



            _mockClient.Setup(c => c
            .GetDatabase(_productDatabaseSettings.DatabaseName, null))
                .Returns(_mockDB.Object);


            var context = new ProductContext(_productDatabaseSettings);
            var myCollection = context.Products.CollectionNamespace;



            Assert.NotNull(myCollection);

        }

        [Fact]
        public async void MongoDbContext_GetProducts()
        {
            _mockOptions.Setup(c => c.CollectionName).Returns(_productDatabaseSettings.CollectionName);
            _mockOptions.Setup(c => c.DatabaseName).Returns(_productDatabaseSettings.DatabaseName);
            _mockOptions.Setup(c => c.ConnectionString).Returns(_productDatabaseSettings.ConnectionString);
            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.DatabaseName, null)).Returns(_mockDB.Object);

            var context = new ProductContext(_mockOptions.Object);

            ProductRepository productRepository = new ProductRepository(context);

            var result = await productRepository.GetProducts();

            //Assert.Equal(11, result.ToList().Count);

        }

        [Fact]
        public void MongoDbContext_Create()
        {
            _mockOptions.Setup(c => c.CollectionName).Returns(_productDatabaseSettings.CollectionName);
            _mockOptions.Setup(c => c.DatabaseName).Returns(_productDatabaseSettings.DatabaseName);
            _mockOptions.Setup(c => c.ConnectionString).Returns(_productDatabaseSettings.ConnectionString);
            _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.DatabaseName, null)).Returns(_mockDB.Object);

            var context = new ProductContext(_mockOptions.Object);

            ProductRepository productRepository = new ProductRepository(context);

            var result =  productRepository.Create(_product);

            List<Product> productsList =  productRepository.GetProducts().Result.ToList();

            Assert.Equal(_product.Name, productsList[productsList.Count - 1].Name);



        }









    }
}
