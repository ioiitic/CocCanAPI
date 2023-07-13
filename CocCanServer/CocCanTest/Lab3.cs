using CocCanService.DTOs.Product;
using CocCanService.Services;
using NUnit.Framework;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace CocCanTest
{
    public class Tests
    {
       
        readonly IProductService _product;
        [Test]
        public void Test1()
        {
            CreateProductDTO account = new CreateProductDTO();

            account.Name = "Tra da";
            account.Image = "none";
            account.CategoryId=Guid.Parse("8f563b97-3971-45df-bf26-c44c6deda43e");
            account.StoreId = Guid.Parse("674a87f2-35b7-4aa9-9dc8-1ac98942b96a");
           
            var check =_product.CreateProductAsync(account);
            Assert.AreEqual(check, check);

            Assert.Pass();
        }
    }
}