using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Ratings
    {
        [Key]
        public int RatingsId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        [ValidateNever]
        public string RatingsLogo { get; set; }

    }
}
