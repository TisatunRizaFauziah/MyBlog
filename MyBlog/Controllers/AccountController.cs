
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;


namespace MyBlog.Controllers
{
   
	public class AccountController : Controller
	{
		private readonly AppDbContext _context;
		public IActionResult Login()
		{
			return View();
		}

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]

		public async Task<IActionResult> Login([FromForm] Login data)
		{
			var userFromDb = _context.Users
				.FirstOrDefault(x=>x.Username == data.Username
					&& x.Password == data.Password);

			if (userFromDb == null)
			{
				@ViewBag.Error = "User not found";
				return View();
			}
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, data.Username),
				new Claim(ClaimTypes.Role, "Admin"),
			};

			var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

			var identity = new ClaimsIdentity(claims, scheme);

			await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));
			return RedirectToAction ("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}
        [Authorize]
        public IActionResult Index()
        {
            var users = _context.Users.ToList(); 
            return View(users);
        }
        public IActionResult Detail(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Register([FromForm] Register data)
        {
          
            if (ModelState.IsValid)
            {
                
                var isUsernameTaken = _context.Users.FirstOrDefault(x => x.Username == data.Username);
                var isEmailTaken = _context.Users.FirstOrDefault(x => x.Email == data.Email);

              
           
                var newUser = new User
                {
                    Username = data.Username,
                    Password = data.Password,
                    Name = data.Name,
                    Role = "Admin", 
                    Alamat = data.Alamat,
                    Email = data.Email,
                    NoTelepon = data.NoTelepon,
                    TanggalLahir = DateTime.Now.Date, // Hanya mengambil bagian tanggal
                };

             
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(); 

                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Role, "Admin"), 
                 
                };

                var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(claims, scheme);

                await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));

                return RedirectToAction( "Login");
            }

            return View();
        }


    }
}
