﻿using Data.Data;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.ViewModels;
using Utility;
namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Role_Admin)] // adding this means only admin can access below 
    public class MovieController : Controller
    {

       // private readonly ApplicationDbContext _context;  

		//private readonly IMovieRepository _movieRepository;

		private readonly IUnitOfWork _unitOfWork;


		private readonly IWebHostEnvironment _webHostEnvironment;



		public MovieController(/*ApplicationDbContext context,*/ IWebHostEnvironment webHostEnvironment, /*IMovieRepository movieRepository*/ IUnitOfWork unitOfWork)
		{
			//_context = context;
			_webHostEnvironment = webHostEnvironment;
			//_movieRepository = movieRepository;
			_unitOfWork = unitOfWork;

		}

		public IActionResult Index()
        {

            //MovieVM vm = new MovieVM()
            //{
            //	CategoryList = _context.C_Category.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem

            //	{
            //		Text = a.Name,
            //		Value = a.CategoryId.ToString(),

            //	}),

            //	RatingList = _context.R_Ratings.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //	{
            //		Text = a.Name,
            //		Value = a.RatingsId.ToString(),

            //	}),

            //	Movie = new Movie()

            //};


            ////var mList =  _context.M_Movies.ToList();
            //// return View(mList);


            //// Use the repository to get all movies
            //var ListfromRepo = _unitOfWork.MovieUnit.GetAll();
            //return View(ListfromRepo);

            // Use the Unit of Work to retrieve the categories
            var categories = _unitOfWork.CategoryUnit.GetAllCategories().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = a.Name,
                Value = a.CategoryId.ToString(),
            });

            // Use the Unit of Work to retrieve the ratings
            var ratings = _unitOfWork.RatingUnit.GetAll().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = a.Name,
                Value = a.RatingsId.ToString(),
            });

            // Create the ViewModel and assign the data retrieved from the repository
            MovieVM vm = new MovieVM()
            {
                CategoryList = categories,
                RatingList = ratings,
                Movie = new Movie()  // Assuming you want an empty movie object initially
            };

            // Use the repository to get all movies
            var ListfromRepo = _unitOfWork.MovieUnit.GetAll();

            // Return the ViewModel to the view
            return View(ListfromRepo);

        }

		public IActionResult Create()
		{	
            
			//       MovieVM vm = new MovieVM() 
			 //       {
			//  CategoryList = _context.C_Category.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			//  {
			//	  Text = a.Name,
			//	  Value = a.CategoryId.ToString(),

			//  }),

			//  RatingList = _context.R_Ratings.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			//  {
			//	  Text = a.Name,
			//	  Value = a.RatingsId.ToString(),

			//  }),

			//  Movie = new Movie()

		    // };	
			
			//return View(vm);

			var categories = _unitOfWork.CategoryUnit.GetAllCategories().Select (b => new SelectListItem
			{
				Text =b.Name,
				Value = b.CategoryId.ToString(),
			});
			
			var ratings = _unitOfWork.RatingUnit.GetAll().Select(c => new SelectListItem
			{
				Text =c.Name,
				Value = c.RatingsId.ToString(),
			});

			MovieVM vm = new MovieVM() 
			{
			  CategoryList= categories,
			  RatingList= ratings,
			  Movie = new Movie()
			};

			return View(vm);
		}

		// ---------------Below before applying images ---------------------

		//[HttpPost]
		//public IActionResult Create(MovieVM movieVM)
		//{

		//	if (ModelState.IsValid)
		//	{
		//		_context.M_Movies.Add(movieVM.Movie);

		//		_context.SaveChanges();

		//		return RedirectToAction("Index");
		//	}


		//	return View(movieVM);


		//}

		//--------------------------------------------------------------------------------------------------------------------


		[HttpPost]
		public IActionResult Create(MovieVM movieVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{

				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string movieImagePath = Path.Combine(wwwRootPath, @"Images\MovieImages\"); // Location of images

					if (!string.IsNullOrEmpty(movieVM.Movie.Image))
					{

					}
					using (var fileStream = new FileStream(Path.Combine(movieImagePath, filename), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					//movieVM.Movie.Ratings.RatingsLogo = @"\Images\MovieImages\" + filename;

					movieVM.Movie.Image = @"\Images\MovieImages\" + filename;
				}


				//_context.M_Movies.Add(movieVM.Movie);
				//_context.SaveChanges();



				// Use the repository to add the movie

				//_movieRepository.Add(movieVM.Movie);

				_unitOfWork.MovieUnit.Add(movieVM.Movie);

                return RedirectToAction("Index");

			}

			return View(movieVM);
		}

		public IActionResult Edit(int? id)
		{
            // Initialize the MovieVM with categories and ratings

            //         MovieVM vm = new MovieVM()
            //{
            //	CategoryList = _context.C_Category.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //	{
            //		Text = a.Name,
            //		Value = a.CategoryId.ToString(),

            //	}),

            //	RatingList = _context.R_Ratings.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //	{
            //		Text = a.Name,
            //		Value = a.RatingsId.ToString(),

            //	}),

            //	Movie = new Movie()

            //};

            var categories = _unitOfWork.CategoryUnit.GetAllCategories().Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.CategoryId.ToString(),
            });

            var ratings = _unitOfWork.RatingUnit.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.RatingsId.ToString(),
            });

            MovieVM vm = new MovieVM()
            {
                CategoryList = categories,
                RatingList = ratings,
                Movie = new Movie()
            };

            if (id == null)
			{
				return View(vm);
			}
			else
			{
				//vm.Movie = _context.M_Movies.FirstOrDefault(a => a.MovieId == id);
				//return View(vm);



				// lets use the repository
				// vm.Movie = _movieRepository.GetById(id.Value);
				vm.Movie = _unitOfWork.MovieUnit.GetById(id.Value);
				
                // If movie is not found, return NotFound
                if (vm.Movie == null)
                {
                    return NotFound();
                }

                return View(vm);

            }

	

		}

		[HttpPost]
		public IActionResult Edit (MovieVM movieVM, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					// Generate a new file name and set the image path
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);  // Name of the Image extension
					string productPath = Path.Combine(wwwRootPath, @"Images\MovieImages\"); // location of the image 

					// Delete the old image if it exists
					if (!string.IsNullOrEmpty(movieVM.Movie.Image))
					{
						// delete old image 
						var oldImagePath = Path.Combine(wwwRootPath, movieVM.Movie.Image.TrimStart('\\'));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					// Save the new image file
					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					// Update the movie image path
					movieVM.Movie.Image = @"\Images\MovieImages\" + fileName;
				}

				//// Update the movie record in the database
				//_context.M_Movies.Update(movieVM.Movie);
				//_context.SaveChanges();

				//// Redirect to Index after successful update
				//return RedirectToAction("Index");


				//------------using MovieRepository ----------------------------------

				//_movieRepository.Edit(movieVM.Movie);

				_unitOfWork.MovieUnit.Edit(movieVM.Movie);
                return RedirectToAction("Index");

            }
			else
			{
				// If the model state is invalid, return the view with the current movieVM
				return View(movieVM);
			}

		}

		public IActionResult Delete(int? id)
		{
            //MovieVM vm = new MovieVM()
            //{
            //	CategoryList = _context.C_Category.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //	{
            //		Text = a.Name,
            //		Value = a.CategoryId.ToString(),

            //	}),

            //	RatingList = _context.R_Ratings.ToList().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //	{
            //		Text = a.Name,
            //		Value = a.RatingsId.ToString(),

            //	}),

            //	Movie = new Movie()

            //};

            //if (id == null)
            //{
            //	return View(vm);
            //}
            //else
            //{
            //	vm.Movie = _context.M_Movies.FirstOrDefault(a => a.MovieId == id);

            //	return View(vm);

            //}

            // Create a new instance of MovieVM and populate CategoryList and RatingList
            //MovieVM vm = new MovieVM ()
            //{
            //	CategoryList = _context.C_Category
            //		.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //		{
            //			Text = a.Name,
            //			Value = a.CategoryId.ToString()
            //		}).ToList(),

            //	RatingList = _context.R_Ratings
            //		.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //		{
            //			Text = a.Name,
            //			Value = a.RatingsId.ToString()
            //		}).ToList(),

            //	Movie = new Movie()
            //};

            var categories = _unitOfWork.CategoryUnit.GetAllCategories().Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.CategoryId.ToString(),
            });

            var ratings = _unitOfWork.RatingUnit.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.RatingsId.ToString(),
            });

            MovieVM vm = new MovieVM()
            {
                CategoryList = categories,
                RatingList = ratings,
                Movie = new Movie()
            };

            // Check if id is provided
            if (id.HasValue)
				{
					// Fetch the movie based on the provided id
					//vm.Movie = _context.M_Movies.FirstOrDefault(a => a.MovieId == id.Value);

				//---using Movierepository ----------------

				   // vm.Movie = _movieRepository.GetById(id.Value);

				vm.Movie = _unitOfWork.MovieUnit.GetById(id.Value);

					// Check if the movie was found
					if (vm.Movie == null)
					{
						// Handle the case where the movie was not found (e.g., return a 404 page)
						return NotFound();
					}
				}

				// Return the view with the populated view model
				return View(vm);
		}




		
		[HttpPost]
		public IActionResult DeletePost(int? id)
		{
			// Fetch the movie record based on the provided id
			//var obj = _context.M_Movies.FirstOrDefault(s => s.MovieId == id);

			var obj = _unitOfWork.MovieUnit.GetById(id.Value);

			if (obj != null)
			{
				// Get the root path of the wwwroot folder
				string wwwRootPath = _webHostEnvironment.WebRootPath;

				// Check if the movie has an associated image and delete it
				if (!string.IsNullOrEmpty(obj.Image))
				{
					var oldImagePath = Path.Combine(wwwRootPath, obj.Image.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}

				// Remove the movie record from the database
				//_context.M_Movies.Remove(obj);
				//_context.SaveChanges();

				_unitOfWork.MovieUnit.Delete(obj);
			}

			// Redirect to the Index action after successful deletion
			return RedirectToAction("Index");
		}


	}
}
