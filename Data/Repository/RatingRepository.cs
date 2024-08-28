using Data.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RatingRepository : IRatingRepository
    {

        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public void Add(Ratings rating)
        {
            _context.R_Ratings.Add(rating);
            _context.SaveChanges();

        }

        public void Delete(Ratings rating)
        {
            _context.R_Ratings.Remove(rating);
            _context.SaveChanges();
        }

        public void Edit(Ratings rating)
        {
            _context.R_Ratings.Update(rating);
            _context.SaveChanges();
        }

        public IEnumerable<Ratings> GetAll()
        {
           return _context.R_Ratings.ToList();
        }

        public Ratings GetById(int id)
        {
            return _context.R_Ratings.FirstOrDefault(a => a.RatingsId == id);
        }
    }
}
