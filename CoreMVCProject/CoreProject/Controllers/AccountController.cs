using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreProject.Models;
using Microsoft.Extensions.Logging;
using CoreProject.Models.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using CoreProject.Models.AppModel;

namespace CoreProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AppDBContext _context;

        public AccountController(ILogger<AccountController> logger, AppDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {                 
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Login == HttpContext.User.Identity.Name);

            if (user == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Edit(int id, [Bind("Email,PhoneNumber,DateBorn")] User user)
        {
            var userDB = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            userDB.DateBorn = user.DateBorn;
            userDB.Email = user.Email;
            userDB.PhoneNumber = user.PhoneNumber;           
          
            try
            {
                _context.Update(userDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        }

        public IActionResult ChangeLogin()
        {
           var user =  _context.Users
                .FirstOrDefault(m => m.Login == HttpContext.User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLogin(string Login)
        {
            var user = _context.Users
                .FirstOrDefault(m => m.Login == HttpContext.User.Identity.Name);

            if (user == null)
            {               
                return NotFound();
            }

            var hasNane = _context.Users
                .FirstOrDefault(m => m.Login == Login);

            if (hasNane != null)
            {
                ModelState.AddModelError("", $"Login {Login} already use another user");
                return View(user);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            user.Login = Login;
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");          
        }

        public IActionResult ChangePassword()
        {                        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
        {
            var user = _context.Users
                .FirstOrDefault(m => m.Login == HttpContext.User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Password != model.OldPassword)
            {
                ModelState.AddModelError("", "Old password is not correct");
                return View(model);
            }

            if (model.ConfirmPassword != model.Password)
            {
                ModelState.AddModelError("", "Passwords in not equels");
                return View(model);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            user.Password = model.Password;
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Include(v=>v.Roles).FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
              
                if (user != null)
                {
                    user.LastLogin = DateTime.UtcNow;
                    await Authenticate(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    user = new User { Login = model.Login, Password = model.Password };
                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                    {
                        user.Roles.Add(userRole);
                        userRole.Users.Add(user);
                    }
                        
                    user.LastLogin = DateTime.UtcNow;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();       

                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", $"{model.Login} already use another user");
            }

            ModelState.AddModelError("", "Inccorect login or password");
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)                
            };

            foreach (var item in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, item.Name));
            }

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
