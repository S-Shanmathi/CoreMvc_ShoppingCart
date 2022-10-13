using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMvc_ShoppingCart.Models;
using System.Web;
using System.Data.SqlClient;






namespace CoreMvc_ShoppingCart.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly ShoppingCartMVCcoreContext _context;

        public UserDetailsController(ShoppingCartMVCcoreContext context)
        {
            _context = context;
        }

        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserDetails.ToListAsync());
        }

        // GET: UserDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetail = await _context.UserDetails
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return View(userDetail);
        }

        // GET: UserDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Password,PhoneNo")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,FirstName,LastName,Password,PhoneNo")] UserDetail userDetail)
        {
            if (id != userDetail.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailExists(userDetail.UserId))
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
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetail = await _context.UserDetails
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UserDetails == null)
            {
                return Problem("Entity set 'ShoppingCartMVCcoreContext.UserDetails'  is null.");
            }
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail != null)
            {
                _context.UserDetails.Remove(userDetail);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDetailExists(string id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string uname, string psw)
        {

            var obj = _context.UserDetails.Where(a => a.UserId.Equals(uname) && a.Password.Equals(psw)).FirstOrDefault();
            if (uname == "admin")
            {
                if (psw == "admin")
                {
                    ViewBag.Text = "Welcome Admin";

                    // redirection's for Admin's action performing page i.e stock ( display | add | edit | delete )
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ViewBag.Text = "Wrong password !!";
                    return RedirectToAction("Login", "UserDetails");
                }
            }
            else if (obj.UserId == uname)
            {
                if (obj.Password == psw)
                {

                    return RedirectToAction("Index", "Carts");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "UserDetails");
                }
            }
            else
            {
                return RedirectToAction("Login", "UserDetails");
            }
            return View();

        }


        public ActionResult forgetpassword(string uname)
        {

            //ShoppingCart1Entities1 db = new ShoppingCart1Entities1();
            var data = _context.UserDetails.Where(a => a.UserId.Equals(uname)).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult forgetpassword(string psw, string uname)
        {
            //ShoppingCart1Entities1 db = new ShoppingCart1Entities1();
            var obj = _context.UserDetails.Where(a => a.UserId.Equals(uname)).FirstOrDefault();
            if (uname == obj.UserId)
            {
                UserDetail dt = new UserDetail();
                var sql = @"update UserDetails set Password='{psw}' where User_id='{uname}'";
                
               _context.Database.ExecuteSqlRaw($"update UserDetails set Password='{psw}' where User_id='{uname}'");
         
                return RedirectToAction("Login","UserDetails");
            }
            else
            {
                return RedirectToAction("forgetpassword","UserDetails");
            }


        }

    }
}
