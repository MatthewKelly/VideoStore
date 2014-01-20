using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using log4net;
using log4net.Repository.Hierarchy;
using MoviesLibrary;
using VideoStore.Caching;
using VideoStore.Mappers;
using VideoStore.Models;

namespace VideoStore.Repositories
{
    public class MovieRepository
    {
        private readonly MovieDataSource _movieDataSource;
        
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(MovieRepository));

        public MovieRepository()
        {
            _movieDataSource = new MovieDataSource();
        }

        public MovieRepository(MovieDataSource movieDataSource)
        {
            _movieDataSource = movieDataSource;
        }

        public List<Movie> GetAllMovies()
        {
            Logger.Info("Downloading movies from database");
            List<MovieData> movieData;
            try
            {
                movieData = _movieDataSource.GetAllData();
            }
            catch (Exception e)
            {
                Logger.Error("Error while downloading movies from database", e);
                throw;
            }
            
            var movies = movieData.Select(x => x.ToMovie());
            return movies.ToList();
        }

        public int CreateMovie(Movie movie)
        {
            if (movie == null) 
                throw new ArgumentNullException();
            var movieData = movie.ToMovieData();
            int movieId;
            try
            {
                movieId = _movieDataSource.Create(movieData);
            }
            catch (Exception e)
            {
                Logger.Error("Error while inserting movie into the database", e);
                throw;
            }
            return movieId;
        }

        public void UpdateMovie(Movie movie)
        {
            if (movie == null) 
                throw new ArgumentNullException();

            var movieData = movie.ToMovieData();
            Logger.Info("Updating movie with id: " + movieData.MovieId);
            try
            {
                _movieDataSource.Update(movieData);
            }
            catch (Exception e)
            {
                Logger.Error("Error while updating movie with id: " + movieData.MovieId, e);
                throw;
            }
            
        }


    }
}