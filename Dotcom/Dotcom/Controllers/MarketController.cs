using Dotcom.Models;
using Dotcom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dotcom.Controllers
{
    [Authorize]
    public class MarketController : Controller
    {
        private MarketContext db;

        public MarketController(MarketContext context)
        {
            db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Claims == null || !User.Claims.Any())
            {
                BagAllProducts();
                return View("Buyer/Marketplace");
            }
            if (User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == "2")
            {
                BagProducts();
                return View("Seller/Management");
            }
            else
            {
                BagAllProducts();
                return View("Buyer/Marketplace");
            }
        }

        [Authorize(Roles = "2")]
        public IActionResult Management()
        {
            BagProducts();
            return View("Seller/Management");
        }

        [AllowAnonymous]
        public IActionResult Marketplace()
        {
            BagAllProducts();
            return View("Buyer/Marketplace");
        }

        private void BagProducts()
        {
            string id = User.Claims.First(c => c.Type == "id").Value;
            ViewBag.Products = db.Products.Where(
                x => x.UId.ToString() == id);
        }

        private void BagAllProducts()
        {
            int id = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? "0");

            ViewBag.Products = (
                from p in db.Products
                join cp in db.CartProducts on 
                    new { pid = p.Id, uid = id } equals 
                    new { pid = cp.PId, uid = cp.UId }
                    into ps

                from cp in ps.DefaultIfEmpty()
                select new Tuple<Product, CartProduct>(p, cp)
                ).Take(25).ToArray();
        }

        [Authorize(Roles = "2")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            db.Products.Remove(db.Products.First(x => x.Id == id));
            await db.SaveChangesAsync();

            BagProducts();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> NewProductAdd(NewProductModel model)
        {
            db.Products.Add(
                new Product 
                { 
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price, Quantity = model.Quantity,
                    UId = Convert.ToInt32(User.Claims.First(x=>x.Type=="id").Value) 
                }
            );

            await db.SaveChangesAsync();

            BagProducts();
            return RedirectToAction("Index");
        }
    }
}
