using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;
using VideoStore.Mappers;
using VideoStore.Models;

namespace VideoStore.Repositories
{
    public class MovieRepository
    {
        private readonly MovieDataSource _movieDataSource = new MovieDataSource();
        
        public List<Movie> GetAllMovies()
        {
            var movieData = _movieDataSource.GetAllData();
            var movies = movieData.Select(x => x.ToMovie());
            return movies.ToList();
        }

        public int CreateMovie(Movie movie)
        {
            var movieData = movie.ToMovieData();
            return _movieDataSource.Create(movieData);
        }

        public void UpdateMovie(Movie movie)
        {
            var movieData = movie.ToMovieData();
            _movieDataSource.Update(movieData);
        }


    }
}