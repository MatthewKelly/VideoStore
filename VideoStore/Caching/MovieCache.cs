using System;
using System.Collections.Generic;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Caching
{
    public class MovieCache
    {
        private static List<Movie> _movies;
        private static DateTime? _expiryDate;
        private readonly MovieRepository _movieRepository = new MovieRepository();

        public List<Movie> AllMovies()
        {
            if (_movies != null && (_expiryDate == null || !(_expiryDate < DateTime.Now))) return _movies;

            _movies = _movieRepository.GetAllMovies();
            _expiryDate = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            return _movies;   
        }


    }
}