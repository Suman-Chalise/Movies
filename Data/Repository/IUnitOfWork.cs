using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    /// <summary>
    /// Adding all Repository in to unitofWork makes it easy so that we only need to inuect UnitofWork rather than individual repository
    /// </summary>
    public interface IUnitOfWork
    {

        
        ICategoryRepository CategoryUnit { get; }

        IRatingRepository RatingUnit { get; }

        IMovieRepository MovieUnit { get; }


    }
}
