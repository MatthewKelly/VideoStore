using System;
using System.Linq;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Operations

{
    public class UpdateMovieHandler
    {
        private readonly MovieRepository _movieRepository;
        private readonly MovieCache _movieCache;

        public UpdateMovieHandler(MovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        public void UpdateMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException();
            if (_movieCache.AllMovies().SingleOrDefault(x => x.MovieId == movie.MovieId) == null)
                throw new InvalidOperationException("Cannot update movie because it does not exist");

            _movieRepository.UpdateMovie(movie);
        
        }

    }
}