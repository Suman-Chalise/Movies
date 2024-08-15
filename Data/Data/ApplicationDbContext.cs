using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        DbSet<Movie>M_Movies { get; set; }
        DbSet<Category>C_Category { get; set; }

        DbSet<Ratings>R_Ratings { get; set; }

    }
}
