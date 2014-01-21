using System;
using System.Collections.Generic;
using System.Linq;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Caching
{
    public class MovieCache
    {
        private static List<Movie> _movies;
        private static DateTime? _expiryDate;
        private readonly IMovieRepository _movieRepository = new MovieRepository();

        public MovieCache()
        {
           _movieRepository = new MovieRepository();
        }

        public MovieCache(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public DateTime? ExpiryDate {
            get { return _expiryDate; }
        }

        public void Expire()
        {
            _movies = null;
        }

        public List<Movie> AllMovies()
        {
            if (_movies != null && (_expiryDate == null || !(_expiryDate < DateTime.Now))) return _movies;

            _movies = _movieRepository.GetAllMovies();
            _expiryDate = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            return _movies;   
        }

        public void UpdateMovieInCache(Movie movie)
        {
            
            if (movie == null)
                throw new ArgumentNullException("movie");

            var movies = AllMovies();
            var matchedMovie = movies.Single(x => x.MovieId == movie.MovieId);
            movies.Remove(matchedMovie);
            movies.Add(movie);
        }


        public void AddMovieToCache(Movie movie) 
        {
            if (movie == null)
                throw new ArgumentNullException("movie");

            var movies = AllMovies();
            movies.Add(movie);
        }

    }
}