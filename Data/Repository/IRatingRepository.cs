using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRatingRepository
    {
        IEnumerable<Ratings> GetAll();

        Ratings GetById(int id);

        void Add(Ratings rating);

        void Edit(Ratings rating);

        void Delete(Ratings rating);
    }
}
