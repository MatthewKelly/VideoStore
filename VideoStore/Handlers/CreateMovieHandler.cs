using System;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Handlers
{
    public class CreateMovieHandler
    {
        private readonly MovieRepository _movieRepository;
        private readonly MovieCache _movieCache;

        public CreateMovieHandler(MovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        public int CreateMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException();
            var newMovieId = _movieRepository.CreateMovie(movie);
            movie.MovieId = newMovieId;
            _movieCache.AddMovieToCache(movie);
            return newMovieId;
        }

    }
}