using System;
using System.Linq;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.MovieHelpers;
using VideoStore.Repositories;

namespace VideoStore.Handlers
{
    public class MovieQueryHandler
    {
        private readonly MovieRepository _movieRepository;
        private readonly MovieCache _movieCache;

        public MovieQueryHandler(MovieRepository movieRepository, MovieCache movieCache)
        {
            _movieRepository = movieRepository;
            _movieCache = movieCache;
        }

        public SearchResult RunMovieQuery(SearchCriteria searchCriteria,
                                          MovieAttribute sortAttribute,
                                          bool sortDesc,
                                          int startFrom,
                                          int pageSize)
        {
            var movies = _movieCache.AllMovies();

            if (searchCriteria != null)
                movies = movies.Search(searchCriteria);

            if (pageSize > 1000)
                throw new InvalidOperationException("Requested page size is too great");

            movies = movies.SortByAttribute(sortAttribute);
            var results = movies.Skip(startFrom)
                                .Take(pageSize)
                                .ToList();

            return new SearchResult
            {
                Results = results,
                TotalSize = movies.Count(),
                StartingFrom = startFrom > movies.Count() ? startFrom : 0,
                Count = results.Count
            };
        }
    }
}