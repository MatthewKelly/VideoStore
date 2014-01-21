using System;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Handlers
{
    public class CreateMovieHandler
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MovieCache _movieCache;

        public CreateMovieHandler(IMovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        public int CreateMovie(Movie movie)
        {
            ValidateMovie(movie);
            movie.MovieId = _movieRepository.CreateMovie(movie);
            _movieCache.AddMovieToCache(movie);
            return movie.MovieId;
        }

        private static void ValidateMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException("movie");
            if (string.IsNullOrEmpty(movie.Title))
                throw new ArgumentException("Movie must contain a title");
        }
    }
}