using Data.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

   

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context; 
            
            CategoryUnit = new CategorRepository(_context);

            RatingUnit = new RatingRepository(_context);

            MovieUnit = new MovieRepository(_context);
        }

        //public ICategoryRepository CategoryUnit => throw new NotImplementedException();

        //public IRatingRepository RatingUnit => throw new NotImplementedException();

        //public IMovieRepository MovieUnit => throw new NotImplementedException();

        public ICategoryRepository CategoryUnit { get; private set; }

        public IRatingRepository RatingUnit { get; private set; }

        public IMovieRepository MovieUnit { get; private set; }
    }
}
