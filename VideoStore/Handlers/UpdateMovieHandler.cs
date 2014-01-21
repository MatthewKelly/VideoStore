using System;
using System.Linq;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Handlers

{
    public class UpdateMovieHandler
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MovieCache _movieCache;

        public UpdateMovieHandler(IMovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        public void UpdateMovie(Movie movie)
        {
            ValidateMovie(movie);
            _movieRepository.UpdateMovie(movie);
            _movieCache.UpdateMovieInCache(movie);
        }

        private void ValidateMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException("movie");
            if (string.IsNullOrEmpty(movie.Title))
                throw new ArgumentException("Movie must have a title");
            if (_movieCache.AllMovies().SingleOrDefault(x => x.MovieId == movie.MovieId) == null)
                throw new InvalidOperationException("Cannot update movie because it does not exist");
        }
    }
}