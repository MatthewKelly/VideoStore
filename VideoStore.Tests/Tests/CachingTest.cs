using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using VideoStore.Caching;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Tests.Tests
{
    public class CachingTest
    {
        [Test]
        public void Cache_Should_Expire_At_Midnight_Local_Time()
        {
            //Setup
            var movieRespository = new Mock<IMovieRepository>();
            movieRespository.Setup(x => x.GetAllMovies()).Returns( new List<Movie> {new Movie()});
            var cache = new MovieCache(movieRespository.Object);
            
            //Act
            cache.AllMovies();

            Assert.That(cache.ExpiryDate == DateTime.Today.AddDays(1).AddMilliseconds(-1));


        }

        [Test]
        public void Cache_Should_Contain_Movie_List()
        {
            //Setup
            var movieRespository = new Mock<IMovieRepository>();
            var movieList = new List<Movie> {new Movie {MovieId = 2}, new Movie {MovieId = 4}};
            movieRespository.Setup(x => x.GetAllMovies()).Returns(movieList);
            var cache = new MovieCache(movieRespository.Object);
            
            //Act
            var result = cache.AllMovies();
            
            CollectionAssert.AreEquivalent(movieList, result);


        }
    }
}
