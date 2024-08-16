using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RatingsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var RatingLists = _context.R_Ratings.ToList();

            return View(RatingLists);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Ratings ratings)
        {

            if (ModelState.IsValid)
            {
                _context.R_Ratings.Add(ratings);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(ratings);
        }


    }
}
