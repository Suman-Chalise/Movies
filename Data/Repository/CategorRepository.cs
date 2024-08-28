using Data.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CategorRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public CategorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Category category)
        {
            _context.C_Category.Add(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.C_Category.Remove(category);
            _context.SaveChanges();
        }

        public Category GetbyId(int id)
        {
            return _context.C_Category.FirstOrDefault(a => a.CategoryId == id);

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.C_Category.ToList();
        }

        public void Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }

        //public void Create(Category category)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(Category category)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Category> GetAllCategories()
        //{
        //    throw new NotImplementedException();
        //}

        //public Category GetbyId(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Category category)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
