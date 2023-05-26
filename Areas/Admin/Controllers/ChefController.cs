using DeliciousMvC.DataContex;
using DeliciousMvC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliciousMvC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly DeliceusDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ChefController(DeliceusDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Chefs>  chef= _context.Chefs.ToList();
            return View(chef);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chefs chefs)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string guid=Guid.NewGuid().ToString();
            string newFile = guid + chefs.Images.FileName;
            string path = Path.Combine(_environment.WebRootPath,"assets","img","chefs",newFile);
            using(FileStream filestream=new FileStream(path, FileMode.CreateNew))
            {
                await chefs.Images.CopyToAsync(filestream);
            }
            Chefs chef = new Chefs()
            {
                ImageName = newFile,
                Name = chefs.Name,
                Work = chefs.Work,

            };
            _context.Chefs.Add(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
                
            
        }
        public IActionResult Delete(int id)
        {
            Chefs chef=_context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "chefs");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);    
            }
            _context.Chefs.Remove(chef);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            Chefs chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            return View(chef);

        }
        public IActionResult Update(int id)
        {
            Chefs chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Chefs chef)
        {
            Chefs chefs = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            string guid =Guid.NewGuid().ToString();
            string newfile = guid + chef.Images.FileName;
            string path = Path.Combine(_environment.WebRootPath,"assets","img","chefs",newfile);
            using(FileStream fileStream=new FileStream(path, FileMode.Create))
            {
                await chef.Images.CopyToAsync(fileStream);
            }
            chefs.ImageName = newfile;
            chefs.Name=chef.Name;
            chefs.Work = chef.Work;
            _context.Chefs.Update(chefs);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
