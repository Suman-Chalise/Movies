
using Data.Data;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Diagnostics.Eventing.Reader;
using Utility;

namespace Movie_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Role_Admin)] // adding this means only admin can access below 
    public class RatingsController : Controller
    {

       // private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;



		// adding below for image upload 

		private readonly IWebHostEnvironment _webHostEnvironment;

        public RatingsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            //_context = context;
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var RatingLists = _unitOfWork.RatingUnit.GetAll();

            return View(RatingLists);
        }

        public IActionResult Add()
        {
            return View();
        }

		//[HttpPost]
		//public IActionResult Add(Ratings ratings)
		//{

		//    if (ModelState.IsValid)
		//    {
		//        _context.R_Ratings.Add(ratings);
		//        _context.SaveChanges();

		//        return RedirectToAction("Index");
		//    }
		//    return View(ratings);
		//}

		[HttpPost]
		public IActionResult Add(Ratings ratings, IFormFile? file)
		{

			if (ModelState.IsValid)
			{

				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if(file != null) 
				{ 
				  string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				 string ratingImagePath = Path.Combine(wwwRootPath, @"Images\ratingImages\"); // Location of images

					if(!string.IsNullOrEmpty(ratings.RatingsLogo)) 
					{ 
					  
					}
					using (var fileStream = new FileStream(Path.Combine(ratingImagePath, filename), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					ratings.RatingsLogo = @"\Images\ratingImages\" + filename;
				}

				//_context.R_Ratings.Add(ratings);
				//_context.SaveChanges();

				_unitOfWork.RatingUnit.Add(ratings);

				return RedirectToAction("Index");

			}
		
			return View(ratings);
		}

		public IActionResult Edit(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}
			else
			{
				//var obj = _context.R_Ratings.FirstOrDefault(x => x.RatingsId == id);
				var obj = _unitOfWork.RatingUnit.GetById(id.Value);
                return View(obj);

			}

			
		}

		[HttpPost]
		public IActionResult Edit(Ratings ratings, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);  // Name of the Image extension
					string productPath = Path.Combine(wwwRootPath, @"Images\ratingImages\"); // location of the image 

					if (!string.IsNullOrEmpty(ratings.RatingsLogo))
					{
						// delete old image 
						var oldImagePath = Path.Combine(wwwRootPath, ratings.RatingsLogo.TrimStart('\\'));

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
					ratings.RatingsLogo = @"\Images\ratingImages\" + fileName;
					

				}
				// Update the movie record in the database
				//_context.R_Ratings.Update(ratings);
				//_context.SaveChanges();

				_unitOfWork.RatingUnit.Edit(ratings);

				// Redirect to Index after successful update
				return RedirectToAction("Index");


			}
			else
			{
				return View(ratings);
			}



		}


		[HttpPost]
		public IActionResult Delete(int? id)
		{
			//if (id == null || id == 0)
			//{
			//	return NotFound();
			//}
			//else
			//{
			//	var obj = _context.R_Ratings.FirstOrDefault(x => x.RatingsId == id);

			//	_context.Remove(obj);
			//	_context.SaveChanges();
			//	return RedirectToAction("Index");


			//}

			//var obj = _context.R_Ratings.FirstOrDefault(a => a.RatingsId == id);

			var obj = _unitOfWork.RatingUnit.GetById(id.Value);

            if (obj != null)
			{
				string wwwrootpath = _webHostEnvironment.WebRootPath;

				if (!string.IsNullOrEmpty(obj.RatingsLogo))
				{
					var oldImagePath = Path.Combine(wwwrootpath, obj.RatingsLogo.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}

				// Remove the movie record from the database
				//_context.R_Ratings.Remove(obj);
				//_context.SaveChanges();

				_unitOfWork.RatingUnit.Delete(obj);
			}

			// Redirect to the Index action after successful deletion
			return RedirectToAction("Index");
		}



	}
}
