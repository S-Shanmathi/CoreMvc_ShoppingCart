using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvc_ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvc_ShoppingCart.Models;

namespace s.Controllers
{
    public class CartsController : Controller
    {
        private readonly ShoppingCartMVCcoreContext _context;

        public CartsController(ShoppingCartMVCcoreContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var ShoppingCartMVCcoreContext = _context.Products.Include(c => c.Category);
            return View(await ShoppingCartMVCcoreContext.ToListAsync());
        }

        public async Task<IActionResult> Cartview()
        {
            var ShoppingCartMVCcoreContext = _context.Carts.Include(c => c.Product);
            return View(await ShoppingCartMVCcoreContext.ToListAsync());
        }
        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public async Task<IActionResult> Create(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            //  ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            //  ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");


            return View(product);
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //   public async Task<IActionResult> Create([Bind("CardId,ProductId,ProductName,ProductCount,ProductTotalprice,UserName")] Product product,int count)
        public async Task<IActionResult> Create(Product product1, int count)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product = await _context.Products.FindAsync(product1.ProductId);
                Cart cart = new Cart();
                if (product.ProductCount != 0)
                {
                    if (product.ProductCount >= count)
                    {

                        cart.ProductId = product1.ProductId;
                        cart.ProductName = product1.ProductName;
                        cart.ProductCount = count;
                        cart.ProductTotalprice = cart.ProductCount * product.ProductPrice;
                        cart.UserName = "Raagul";
                    }
                    else
                    {
                        await Response.WriteAsync("<script>alert('Product doesnt have that much quantity,Try again with different quantity limit.')</script>");
                        return RedirectToAction("Index", "Carts");
                    }
                }
                else
                {
                    await Response.WriteAsync("<script>alert('Product Out of Stock')</script>");
                    return RedirectToAction("Index", "Carts");

                }
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", product1.ProductId);


            return View(product1);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cart.ProductId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cart cart)
        {
            if (id != cart.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product1 = await _context.Products.FindAsync(cart.ProductId);
                    if (product1.ProductCount >= cart.ProductCount)
                    {
                        cart.ProductTotalprice = cart.ProductCount * product1.ProductPrice;
                        _context.Update(cart);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await Response.WriteAsync("<script>alert('Product doesnt have that much quantity,Try again with different quantity limit.')</script>");
                        return RedirectToAction("Create", "Carts");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'ShoppingCartMvccoreContext.Carts'  is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CardId == id);
        }


        public ActionResult Payment()
        {
            {
                string amount = _context.Carts.Sum(w => w.ProductTotalprice).ToString();

                ViewBag.answer = amount;
            }

            return View();
        }
        public ActionResult StockMinus()
        {

            var cartdetails = _context.Carts.Select(w => w.ProductId).ToList();

            for (int i = 0; i < cartdetails.Count; i++)
            {
                _context.Database.ExecuteSqlRaw($"Update Products set PRODUCT_COUNT=(select PRODUCT_COUNT from Products where PRODUCT_ID={cartdetails[i]}) - (select PRODUCT_COUNT from Cart where PRODUCT_ID= {cartdetails[i]})where PRODUCT_ID={cartdetails[i]} ");
            }
            _context.Database.ExecuteSqlRaw("delete Cart");

            return View();
        }

    
    }
}


