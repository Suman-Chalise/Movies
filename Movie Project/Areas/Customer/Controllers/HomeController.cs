using Data.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Models.Models;
using Models.ViewModels;

namespace Movie_Project.Area.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
     
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Movie> movieList = _context.M_Movies.ToList();
            return View(movieList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Details(int? id)
		{
			MovieVM vm = new MovieVM()
			{
				CategoryList = _context.C_Category.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = a.Name,
					Value = a.CategoryId.ToString(),

				}),

				RatingList = _context.R_Ratings.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = a.Name,
					Value = a.RatingsId.ToString(),

				}),

				Movie = new Movie()

			};

			if (id == null)
			{
				return View(vm);
			}
			else
			{
				vm.Movie = _context.M_Movies.FirstOrDefault(a => a.MovieId == id);

				return View(vm);

			}



		}

	}
}
