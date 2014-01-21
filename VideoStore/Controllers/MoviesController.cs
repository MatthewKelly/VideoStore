using System.Web.Http;
using VideoStore.Caching;
using VideoStore.Handlers;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Controllers
{
    public class MoviesController : ApiController
    {

        private readonly IMovieRepository _movieRepository;
        private readonly MovieCache _movieCache;
        public MoviesController()
        {
            _movieRepository = new MovieRepository();
            _movieCache = new MovieCache();
        }

        public MoviesController(IMovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        // GET api/values
        public SearchResult Get([FromUri] SearchCriteria searchCriteria, 
                                          MovieAttribute sortAttribute = MovieAttribute.Title, 
                                          bool sortDesc = false, 
                                          int startFrom = 0, 
                                          int pageSize = 100)
        {
            var handler = new MovieQueryHandler(_movieCache);
            var result = handler.RunMovieQuery(searchCriteria, sortAttribute, sortDesc, startFrom, pageSize);
            return result;

        }


        // POST api/values
        public int Post([FromBody]Movie movie)
        {
            var handler = new CreateMovieHandler(_movieRepository, _movieCache);
            var newMovieId = handler.CreateMovie(movie);
            return newMovieId;
        }

        // PUT api/values
        public void Put([FromBody] Movie movie)
        {
            var handler = new UpdateMovieHandler(_movieRepository, _movieCache);
            handler.UpdateMovie(movie);
        }

    }
}