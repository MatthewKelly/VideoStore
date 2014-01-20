using System;
using System.Collections.Generic;
using System.Linq;
using VideoStore.Models;

namespace VideoStore.MovieHelpers
{
    public static class SortingHelper
    {
        public static List<Movie> SortByAttribute(this List<Movie> movies, MovieAttribute attribute, bool sortDesc = false)
        {
            var sortedList = SortMoviesInAscendingOrder(movies, attribute);
 
            if (sortedList == null) return null;
            if (sortDesc)
                sortedList = (IOrderedEnumerable<Movie>) sortedList.Reverse();
            return sortedList.ToList();
        }

        private static IOrderedEnumerable<Movie> SortMoviesInAscendingOrder(IEnumerable<Movie> movies, MovieAttribute attribute)
        {
            IOrderedEnumerable<Movie> sortedList = null;
            switch (attribute)
            {
                case MovieAttribute.Cast:
                    throw new InvalidOperationException("Cannot sort by the cast field");
                case MovieAttribute.Classification:
                    sortedList = movies.OrderBy(x => x.Classification);
                    break;
                case MovieAttribute.Genre:
                    sortedList = movies.OrderBy(x => x.Genre);
                    break;
                case MovieAttribute.MovieId:
                    sortedList = movies.OrderBy(x => x.MovieId);
                    break;
                case MovieAttribute.Rating:
                    sortedList = movies.OrderBy(x => x.Rating);
                    break;
                case MovieAttribute.ReleaseDate:
                    sortedList = movies.OrderBy(x => x.ReleaseDate);
                    break;
                case MovieAttribute.Title:
                    sortedList = movies.OrderBy(x => x.Title);
                    break;
            }
            return sortedList;
        }
    }
}