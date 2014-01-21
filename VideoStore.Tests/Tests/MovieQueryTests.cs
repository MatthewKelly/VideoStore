using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using VideoStore.Caching;
using VideoStore.Handlers;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Tests.Tests
{
    public class MovieQueryTests
    {
        public static MovieQueryHandler GetMovieQueryHandler(List<Movie> movies)
        {
            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(x => x.GetAllMovies()).Returns(movies);
            var movieCache = new MovieCache(movieRepository.Object);
            movieCache.Expire();
            var movieQueryHandler = new MovieQueryHandler(movieCache);
            return movieQueryHandler;
        }

        [Test]
        public void Query_Returns_The_Requested_Number_Of_Results()
        {
            // Setup
            var movieQueryHandler = GetMovieQueryHandler(new List<Movie>{ new Movie(), new Movie(), new Movie()});
            var requestedNumberOfResults = 1;

            //Act
            var results = movieQueryHandler.RunMovieQuery(null, MovieAttribute.Title, false, 0, requestedNumberOfResults);
            

            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results.Results.Count, 1);
            Assert.AreEqual(results.StartingFrom, 0);
        }

        [Test]
        public void Query_Results_Starts_From_The_Requested_Record()
        {
            // Setup
            var movieQueryHandler = GetMovieQueryHandler(new List<Movie> { new Movie(), new Movie(), new Movie() , new Movie()});
            var startsFrom = 1;

            //Act
            var results = movieQueryHandler.RunMovieQuery(null, MovieAttribute.Title, false, startsFrom, 1000);


            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results.TotalSize, 4);
            Assert.AreEqual(results.Results.Count, 3);
            Assert.AreEqual(results.StartingFrom, 1);
        }



        [Test]
        public void Exception_Is_Thrown_If_Page_Size_Is_Greater_Than_1000_Or_Less_Than_1()
        {
            // Setup
            var movieQueryHandler = GetMovieQueryHandler(new List<Movie> { new Movie(), new Movie(), new Movie(), new Movie() });
            var smallPageSize = 0;
            var largePageSize = 1001;

            //Act
            Assert.Throws(typeof(ArgumentException), () => movieQueryHandler.RunMovieQuery(null, MovieAttribute.Title, false, 1, smallPageSize));
            Assert.Throws(typeof(ArgumentException), () => movieQueryHandler.RunMovieQuery(null, MovieAttribute.Title, false, 1, largePageSize));
        }



        [Test]
        public void Query_Returns_Results_With_Correct_Sorting_For_Title()
        {
            // Setup
            var movies = new List<Movie>
            {
                new Movie {Title = "Limestone"},
                new Movie {Title = "Amethyst"},
                new Movie {Title = "Pearl"}
            };
 
            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(null, MovieAttribute.Title, false, 0, 100).Results;

            Assert.IsTrue(results.ElementAt(0).Title == "Amethyst");
            Assert.IsTrue(results.ElementAt(1).Title == "Limestone");
            Assert.IsTrue(results.ElementAt(2).Title == "Pearl");
        }


        [Test]
        public void Query_Returns_Results_With_Correct_Sorting_For_Movie_Id()
        {
            // Setup
            var movies = new List<Movie>
            {
                new Movie {MovieId = 500},
                new Movie {MovieId = 400},
                new Movie {MovieId = 300}
            };

            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(null, MovieAttribute.MovieId, false, 0, 100).Results;

            Assert.IsTrue(results.ElementAt(0).MovieId == 300);
            Assert.IsTrue(results.ElementAt(1).MovieId == 400);
            Assert.IsTrue(results.ElementAt(2).MovieId == 500);

        }

        [Test]
        public void Query_Returns_Results_For_Release_Date_With_Descending_Order()
        {
            // Setup
            var movies = new List<Movie>
            {
                new Movie {ReleaseDate = 2013},
                new Movie {ReleaseDate = 2015},
                new Movie {ReleaseDate = 2016}
            };

            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(null, MovieAttribute.ReleaseDate, true, 0, 100).Results;

            Assert.IsTrue(results.ElementAt(0).ReleaseDate == 2016);
            Assert.IsTrue(results.ElementAt(1).ReleaseDate == 2015);
            Assert.IsTrue(results.ElementAt(2).ReleaseDate == 2013);

        }

        [Test]
        public void Search_Returns_Correct_Result()
        {
            // Setup
            var targetMovie = new Movie{ Title = "The Day After Tomorrow"};
            var movies = new List<Movie>
            {
                targetMovie,
                new Movie {Title = "Some Other Movie"},
                new Movie {Title = "Another Movie"}
            };

            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(new SearchCriteria{Title = "The Day After Tomorrow"}, MovieAttribute.ReleaseDate, true, 0, 100).Results;
            Assert.That(results.Single() == targetMovie);

        }
        [Test]
        public void Search_Returns_Multiple_Matches_Correctly()
        {
            // Setup
            var movies = new List<Movie>
            {
                new Movie {Classification = "M"},
                new Movie {Classification = "MA"},
                new Movie {Classification = "MA"},
                new Movie {Classification = "G"}
            };

            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(new SearchCriteria { Classification = "MA"}, MovieAttribute.ReleaseDate, true, 0, 100).Results;
            Assert.That(results.Count() == 2);
            Assert.That(results.TrueForAll(x => x.Classification == "MA"));

        }


        [Test]
        public void Search_Should_Correctly_Search_By_Genre()
        {
            // Setup
            var movies = new List<Movie>
            {
                new Movie {Genre = "Comedy"},
                new Movie {Genre = "Sports"},
                new Movie {Genre = "Action"},
                new Movie {Genre = "Sports"}
            };

            var movieQueryHandler = GetMovieQueryHandler(movies);

            //Act
            var results = movieQueryHandler.RunMovieQuery(new SearchCriteria { Genre = "Sports" }, MovieAttribute.ReleaseDate, true, 0, 100).Results;
            Assert.That(results.Count() == 2);
            Assert.That(results.TrueForAll(x => x.Genre == "Sports"));

        }


    }
}
