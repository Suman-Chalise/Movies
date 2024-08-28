using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();

        Category GetbyId(int id);

        void Create(Category category);

        void Update(Category category);

        void Delete(Category category);

    
    }
}
