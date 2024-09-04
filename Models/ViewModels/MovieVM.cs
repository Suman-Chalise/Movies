using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
	public class MovieVM
	{

		public Movie Movie { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> RatingList { get; set; }


	}
}
