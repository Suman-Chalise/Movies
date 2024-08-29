using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Data.Data
{
   // public class ApplicationDbContext : DbContext

   // To implemenmt Identity we need to use identity  then only be able to use scaffolding and select this applictaiondbcontext.
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        public DbSet<Movie>M_Movies { get; set; }
        public  DbSet<Category>C_Category { get; set; }

       public DbSet<Ratings>R_Ratings { get; set; }

        // adding new Applictationuser table into database 

       public DbSet<ApplicationUser>A_ApplicationUsers { get; set; }

    }
}
