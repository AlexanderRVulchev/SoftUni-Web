using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVC_Intro_Demo.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace MVC_Intro_Demo.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = this.products
                    .Where(p => p.Name.ToLower()
                        .Contains(keyword.ToLower()));
                return View(foundProducts);
            }
            return View(this.products);
        }

        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        public IActionResult AllAsText()
        {
            var products = this.products.ToList();
            string result = String.Join("\r\n", products.Select(p => $"Product: {p.Id}: {p.Name} - {p.Price}lv."));
            return Content(result);
        }

        public IActionResult AllAsJson()
        {
            return Json(this.products, new JsonSerializerOptions { WriteIndented = true });
        }

        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new();
            foreach (var p in this.products)
            {
                sb.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:f2}lv.");
            }
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment; filename=products.txt");
            byte[] fileContents = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd());
            string contentType = "text/plain";
            return File(fileContents, contentType);
        }

        private IEnumerable<ProductViewModel> products
            = new List<ProductViewModel>()
            {
                new ProductViewModel
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00m
                },
                new ProductViewModel
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50m
                },
                new ProductViewModel
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.50m
                }
            };
    }
}
