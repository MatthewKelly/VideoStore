using System.Collections.Generic;
using System.Web.Http;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.MovieHelpers;
using VideoStore.Repositories;

namespace VideoStore.Controllers
{
    public class MoviesController : ApiController
    {

        private readonly MovieRepository _movieRepository = new MovieRepository();
        private readonly MovieCache _movieCache = new MovieCache();
        public MoviesController()
        {
            _movieRepository = new MovieRepository();
        }

        // GET api/values
        public IEnumerable<Movie> Get(MovieAttributes movieAttribute)
        {
            var movies = _movieCache.AllMovies();
            return movies.SortByAttribute(movieAttribute);
        }


        // POST api/values
        public void Post(Movie movie)
        {
            _movieRepository.CreateMovie(movie);
        }

        // PUT api/values/5
        public void Put(Movie movie)
        {
            _movieRepository.UpdateMovie(movie);
        }

    }
}