using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using VideoStore.Caching;
using VideoStore.Controllers;
using VideoStore.Handlers;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Tests.Tests
{
    public class ControllerTests
    {
        public static MovieCache GetMovieCache(List<Movie> movies)
        {
            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(x => x.GetAllMovies()).Returns(movies);
            var movieCache = new MovieCache(movieRepository.Object);
            movieCache.Expire();
            return movieCache;
        }

        [Test]
        public void Controller_Get_Returns_The_Requested_Number_Of_Results()
        {
            // Setup
            var movieCache = GetMovieCache(new List<Movie> { new Movie(), new Movie(), new Movie() });
            var requestedNumberOfResults = 1;
            var controller = new MoviesController(null, movieCache);
            
            //Act
            var results = controller.Get(null, MovieAttribute.Title, false, 0, requestedNumberOfResults);


            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results.Results.Count, 1);
            Assert.AreEqual(results.StartingFrom, 0);
        }
    }
}
