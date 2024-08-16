using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          var mList =  _context.M_Movies.ToList();

            return View(mList);
        }
    }
}
