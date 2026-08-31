using ASP_net_Project.Models;
using Microsoft.AspNetCore.Mvc;


namespace ASP_net_Project.Controllers
{
    public class AccountController : Controller
    {

        private readonly LfmsContext _context;


        public AccountController(LfmsContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Name, string Password)
        {
            if (string.IsNullOrEmpty(Name))
            {
                ViewBag.ErrorMessage = "Name is required";
                return View();
            }

            if (string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "Password is required";
                return View();
            }

            var existingUser = _context.Users.FirstOrDefault(
                x => x.Name == Name && x.Password == Password
            );

            if (existingUser != null)
            {
                HttpContext.Session.SetInt32("UserId", existingUser.Id);
                HttpContext.Session.SetString("UserName", existingUser.Name);

                return RedirectToAction("MainView", "Item");
            }

            ViewBag.ErrorMessage = "Invalid username or password";

            return View();
        }





        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult EditRegister()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }



        [HttpPost]
        public IActionResult EditRegister(User user)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var existingUser = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (existingUser == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                user.Password = existingUser.Password;
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            _context.SaveChanges();

            HttpContext.Session.SetString("UserName", existingUser.Name);

            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }


        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            return View(user);
        }


    }
}
