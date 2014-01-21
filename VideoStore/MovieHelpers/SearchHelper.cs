using System.Collections.Generic;
using System.Linq;
using VideoStore.Models;

namespace VideoStore.MovieHelpers
{
    public static class SearchHelper
    {

        public static List<Movie> Search(this List<Movie> movieData, SearchCriteria searchCriteria)
        {
            IEnumerable<Movie> movieResults = movieData;

            movieResults = FilterMovies(searchCriteria, movieResults);

            return movieResults.ToList();

        }

        private static IEnumerable<Movie> FilterMovies(SearchCriteria searchCriteria, IEnumerable<Movie> movies)
        {
            if (!string.IsNullOrEmpty(searchCriteria.Cast))
                movies = movies.Where(x => x.Cast.Contains(searchCriteria.Cast));

            if (!string.IsNullOrEmpty(searchCriteria.Classification))
                movies = movies.Where(x => x.Classification.Contains(searchCriteria.Classification));

            if (!string.IsNullOrEmpty(searchCriteria.Genre))
                movies = movies.Where(x => x.Genre.Contains(searchCriteria.Genre));

            if (searchCriteria.Rating != null)
                movies = movies.Where(x => x.Rating == searchCriteria.Rating);

            if (searchCriteria.ReleaseDate != null)
                movies = movies.Where(x => x.ReleaseDate > searchCriteria.ReleaseDate);

            if (!string.IsNullOrEmpty(searchCriteria.Title))
                movies = movies.Where(x => x.Title.Contains(searchCriteria.Title));

            return movies;
        }
    }
}