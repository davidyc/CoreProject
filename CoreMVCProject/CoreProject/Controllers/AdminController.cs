using CoreProject.Models;
using CoreProject.Models.AppModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    [Authorize(Roles = "davidyc")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly AppDBContext _context;

        public AdminController(ILogger<AdminController> logger, AppDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(x=>x.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Login,Email,Password")] User user)
        {
            var userDB = _context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (userDB != null)
            {
                ModelState.AddModelError("",$"{user.Login} already use another user");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                if (userRole != null)
                {
                    user.Roles.Add(userRole);
                    userRole.Users.Add(user);
                }
                
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,PhoneNumber,DateBorn,Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userDB = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                    userDB.Login = user.Login;
                    userDB.Password = user.Password;
                    userDB.PhoneNumber = user.PhoneNumber;
                    userDB.Email = user.Email;
                    userDB.DateBorn = user.DateBorn;

                    _context.Update(userDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
