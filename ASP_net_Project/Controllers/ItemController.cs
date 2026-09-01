using ASP_net_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_net_Project.Controllers
{
    public class ItemController : Controller
    {

        private readonly LfmsContext _context;


        public ItemController(LfmsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            return View(items);
        }


        public IActionResult MainView()
        {
            ViewBag.Users = _context.Users.ToList();
            var recentItems = _context.Items.OrderByDescending(x => x.Id).Take(6).ToList();

            return View(recentItems);
        }



        public IActionResult Founds()
        {
            
            var foundItems = _context.Items.ToList();
            ViewBag.Users = _context.Users.ToList();

            return View(foundItems);
        }


        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.ErrorMessage = "You must login to report a lost item.";
                return View();
            }

            return View();
        }



        [HttpPost]
        public IActionResult Add(Item item)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.ErrorMessage = "You must login to report a lost item.";
                return View(item);
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            item.UserId = HttpContext.Session.GetInt32("UserId").Value;

            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("MainView");
        }




        public IActionResult DeleteItem(int ItemId )
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.Items.FirstOrDefault(i => i.Id == ItemId);

            if (item == null)
            {
                return RedirectToAction("Founds");
            }
                
            if (userId != 1 && item.UserId != userId)
            {
                return RedirectToAction("Founds");
            }

            
                _context.Items.Remove(item);
                _context.SaveChanges();

            
          

            return RedirectToAction("Founds");

        }


        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return RedirectToAction("Founds");
            }

            if (userId != 1 && item.UserId != userId)
            {
                return RedirectToAction("Founds");
            }

            return View(item);
        }


        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem == null)
            {
                return RedirectToAction("Founds");
            }

            if (userId != 1 && existingItem.UserId != userId)
            {
                return RedirectToAction("Founds");
            }

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Location = item.Location;
            existingItem.IsAvailable = item.IsAvailable;

            _context.SaveChanges();

            return RedirectToAction("Founds");
        }


        [HttpPost]
        public IActionResult SearchItems(string search)
        {
            ViewBag.SearchTerm = search;
            var items = _context.Items.Where(x => x.Name.Contains(search) || x.Location.Contains(search)).ToList();
            ViewBag.Users = _context.Users.ToList();

            return View("Search", items);
        }


        public IActionResult MyReports()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var items = _context.Items.Where(x => x.UserId == userId).ToList();

            return View("Founds",items);
        }

    }
}
