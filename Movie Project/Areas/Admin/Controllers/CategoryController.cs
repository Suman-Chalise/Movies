using Data.Data;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        //private readonly ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        

        public CategoryController(/*ApplicationDbContext context,*/ IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // var CatList = _context.C_Category.ToList();

            var CatList = _unitOfWork.CategoryUnit.GetAllCategories();

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
                //_context.C_Category.Add(category);
                //_context.SaveChanges();

                _unitOfWork.CategoryUnit.Create(category);
                return RedirectToAction("Index");
            }

			return View(category);
		}

        public IActionResult Edit(int? id)
        {

            //var obj =   _context.C_Category.FirstOrDefault(q => q.CategoryId == id);


            var obj = _unitOfWork.CategoryUnit.GetbyId(id.Value);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                
                //_context.C_Category.Update(category);
                //_context.SaveChanges();
                _unitOfWork.CategoryUnit.Update(category);

               return RedirectToAction ("Index");
            }

            return View(category);

        }

        public IActionResult Delete(int? id)
        {
            //var obj = _context.C_Category.FirstOrDefault(q => q.CategoryId == id);


            if (id == null)
            {
                return BadRequest("Category ID cannot be null.");
            }

            var obj = _unitOfWork.CategoryUnit.GetbyId(id.Value);

            if (obj == null)
            {
                return NotFound("Category not found.");
            }

            return View(obj);
        }

        [HttpPost]
		public IActionResult DeletePost(Category category)
		{
			

			//_context.C_Category.Remove(category);

           //  _context.SaveChanges();

            _unitOfWork.CategoryUnit.Delete(category);

            return RedirectToAction ("Index");
			
		}



	}
}
