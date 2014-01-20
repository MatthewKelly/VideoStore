using System.Collections.Generic;
using System.Web.Http;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.MovieHelpers;

namespace VideoStore.Controllers
{
    public class MovieSearchController : ApiController
    {
        // GET api/moviesearch
        private readonly MovieCache _movieCache = new MovieCache();

        public IEnumerable<Movie> Get(SearchCriteria searchCriteria)
        {
            var movies = _movieCache.AllMovies();
            return movies.Search(searchCriteria);
        }




    }
}
