using Dotcom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotcom.Controllers
{
    public class CartController : Controller
    {
        private CartContext db;

        public CartController(CartContext context)
        {
            db = context;
        }

        [Authorize(Roles = "3")]
        public IActionResult Index()
        {
            BagProducts();
            return View("Index");
        }

        private void BagProducts()
        {
            string id = User.Claims.First(c => c.Type == "id").Value;

            ViewBag.Products = (
                from cp in db.CartProducts
                join p in db.Products on cp.PId equals p.Id
                where cp.UId.ToString() == id
                select new Tuple<CartProduct, Product>(cp, p)).ToArray();


            ViewBag.Total = 
                0M + (ViewBag.Products as Tuple<CartProduct, Product>[])
                .Sum(x => x.Item2.Price * x.Item1.Quantity);
        }

        [Authorize(Roles = "3")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddToCart(int pid)
        {
            db.CartProducts.Add(
                new CartProduct
                {
                    PId = pid,
                    Quantity = 1,
                    UId = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value)
                }
            );

            await db.SaveChangesAsync();

            BagProducts();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "3")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            db.CartProducts.Remove(db.CartProducts.First(x => x.Id == id));
            await db.SaveChangesAsync();

            BagProducts();
            return RedirectToAction("Index");
        }
    }
}
