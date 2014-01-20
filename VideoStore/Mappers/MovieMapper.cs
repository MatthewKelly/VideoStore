using System;
using MoviesLibrary;
using VideoStore.Models;

namespace VideoStore.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieData movieData)
        {
            return new Movie
            {
                Cast = movieData.Cast,
                Classification = movieData.Classification,
                Genre = movieData.Genre,
                MovieId = movieData.MovieId,
                Rating = movieData.Rating,
                ReleaseDate = new DateTime(movieData.ReleaseDate),
                Title = movieData.Title
            };
        }

        public static MovieData ToMovieData(this Movie movie)
        {
            return new MovieData
            {
                Cast = movie.Cast,
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = movie.MovieId,
                Rating = movie.Rating,
                ReleaseDate = (int) movie.ReleaseDate.Ticks,
                Title = movie.Title
            };

        }
    }
}