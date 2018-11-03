using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product p)
            {
                //do nothing, not required for test
            }
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]

        //[InlineData(275, 48.95, 19.50, 24.95)]
        //[InlineData(5, 48.95, 19.50, 24.95)]
        //public void IndexActionModelIsComplete(decimal pr1, decimal pr2, decimal pr3, decimal pr4)
        public void IndexActionModelIsComplete(Product[] products)
        {
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = products
                //Products = new Product[]
                //{
                //        new Product {Name = "P1", Price = pr1 },
                //        new Product {Name = "P2", Price = pr2 },
                //        new Product {Name = "P3", Price = pr3 },
                //        new Product {Name = "P3", Price = pr4 }
                //}
            };

            var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model,
                Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
                && p1.Price == p2.Price));
        }

        //    class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
        //    {
        //        public IEnumerable<Product> Products { get; } = new Product[]
        //        {
        //            new Product {Name = "P1", Price = 5M },
        //            new Product {Name = "P2", Price = 48.95M },
        //            new Product {Name = "P3", Price = 19.50M },
        //            new Product {Name = "P3", Price = 34.95M }
        //        };

        //        public void AddProduct(Product p)
        //        {
        //            //do nothing, not required for test
        //        }
        //    }

        //    [Fact]
        //    public void IndexActionModelIsCompletePricesUnder50()
        //    {
        //        var controller = new HomeController();
        //        controller.Repository = new ModelCompleteFakeRepositoryPricesUnder50();

        //        var model = (controller.Index() as ViewResult)?.ViewData.Model
        //            as IEnumerable<Product>;

        //        Assert.Equal(controller.Repository.Products, model,
        //            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name &&
        //            p1.Price == p2.Price));
        //    }
        //}
    }
}
