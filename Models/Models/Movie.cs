using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Models.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string? Title { get; set; }

        public DateTime ReleaseDate { get; set; }
     
        public decimal Price { get; set; }

        public string Director { get; set; }

        public string Cast { get; set; }

        public string RunTime { get; set; }

        public int CatId {  get; set; }
        [ForeignKey("CatId")]
		[ValidateNever]
		public Category Category { get; set; }

   
        public int RatId { get; set; }
        [ForeignKey("RatId")]
		[ValidateNever]
		public Ratings Ratings { get; set; }

		[ValidateNever]
		public string Image { get; set; }







	}
}
