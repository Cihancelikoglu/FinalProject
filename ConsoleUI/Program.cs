using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUI
{
    //S -O- LID
    //Open Closed Principle
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(categoryManager.GetById(1).Data.CategoryName);
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();

            if (result.IsSuccess)
            {
                foreach (var productManagerDto in result.Data)
                {
                    Console.WriteLine(productManagerDto.ProductName + " / " + productManagerDto.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }


            //foreach (var item in productManager.GetAllByCategoryId(2).Data)
            //{
            //    Console.WriteLine(item.ProductName);
            //}
        }
    }
}
