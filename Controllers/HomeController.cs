using DeliciousMvC.DataContex;
using DeliciousMvC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliciousMvC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DeliceusDbContext _context;

        public HomeController(DeliceusDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Chefs> chef=_context.Chefs.ToList();
            return View(chef);
        }
    }
}
