using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IMovieRepository 
    {
        IEnumerable<Movie> GetAll();

        Movie GetById(int id);

        void Add(Movie movie);

        void Edit(Movie movie);

        void Delete(Movie movie);


    }
}
