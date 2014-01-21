using System;
using System.Collections.Generic;
using System.Linq;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.MovieHelpers;

namespace VideoStore.Handlers
{
    public class MovieQueryHandler
    {
        private readonly MovieCache _movieCache;

        public MovieQueryHandler(MovieCache movieCache)
        {
            _movieCache = movieCache;
        }

        public SearchResult RunMovieQuery(SearchCriteria searchCriteria,
                                          MovieAttribute sortAttribute,
                                          bool sortDesc,
                                          int startFrom,
                                          int pageSize)
        {

            if (pageSize < 1 || pageSize > 1000)
                throw new ArgumentException("Requested page size is invalid. Page size must be between 1 and 1000", "pageSize");

            var movies = _movieCache.AllMovies();

            if (searchCriteria != null) {
                movies = movies.Search(searchCriteria);
}
            movies = movies.SortByAttribute(sortAttribute, sortDesc);

            var results = PaginateMovies(startFrom, pageSize, movies);

            return new SearchResult
            {
                Results = results,
                TotalSize = movies.Count(),
                StartingFrom = startFrom < movies.Count() ? startFrom : 0,
                Count = results.Count
            };
        }

        private static List<Movie> PaginateMovies(int startFrom, int pageSize, IEnumerable<Movie> movies)
        {
            return movies.Skip(startFrom)
                        .Take(pageSize)
                        .ToList();
        }
    }
}