using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;
using VideoStore.Models;

namespace VideoStore.MovieHelpers
{
    public static class SortingHelper
    {
        public static List<Movie> SortByAttribute(this List<Movie> movies, MovieAttributes attribute)
        {
            IOrderedEnumerable<Movie> sortedList = null;
            switch (attribute) {
                case MovieAttributes.Cast:
                    throw new InvalidOperationException("Cannot sort by the cast field");
                    break;
                case MovieAttributes.Classification:
                    sortedList = movies.OrderBy(x => x.Classification);
                    break;
                case MovieAttributes.Genre:
                    sortedList = movies.OrderBy(x => x.Genre);
                    break;
                case MovieAttributes.MovieId:
                    sortedList = movies.OrderBy(x => x.MovieId);
                    break;
                case MovieAttributes.Rating:
                    sortedList = movies.OrderBy(x => x.Rating);
                    break;
                case MovieAttributes.ReleaseDate:
                    sortedList = movies.OrderBy(x => x.ReleaseDate);
                    break;
                case MovieAttributes.Title:
                    sortedList = movies.OrderBy(x => x.Title);
                    break;
            }
            return sortedList != null ? sortedList.ToList() : null;
        }


    }
}