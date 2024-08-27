using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var CatList = _context.C_Category.ToList();

            return View(CatList);
        }

        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
		public IActionResult Add(Category category)
		{
            if (ModelState.IsValid)
            {
                _context.C_Category.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

			return View(category);
		}

        public IActionResult Edit(int? id)
        {

            var obj =   _context.C_Category.FirstOrDefault(q => q.CategoryId == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _context.C_Category.Update(category);
                _context.SaveChanges();

               return RedirectToAction ("Index");
            }

            return View(category);

        }

        public IActionResult Delete(int id)
        {
			var obj = _context.C_Category.FirstOrDefault(q => q.CategoryId == id);

			return View(obj);
		}

        [HttpPost]
		public IActionResult DeletePost(Category category)
		{
			

			_context.C_Category.Remove(category);

            _context.SaveChanges();

            return RedirectToAction ("Index");
			
		}



	}
}
