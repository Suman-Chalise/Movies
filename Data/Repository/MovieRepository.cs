using Data.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class MovieRepository : IMovieRepository
    {

        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Movie movie)
        {

            _context.Add(movie);
            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(Movie movie)
        {
           _context.Remove(movie);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetAll()
        {
           return _context.M_Movies.ToList();
        }

        public Movie GetById(int id)
        {
           return _context.M_Movies.FirstOrDefault(a => a.MovieId == id);
        }

        public void Edit(Movie movie)
        {
            _context.M_Movies.Update(movie);
            _context.SaveChanges();
        }
    }
}
