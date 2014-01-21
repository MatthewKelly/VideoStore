using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using Moq;
using NUnit.Framework;
using VideoStore.Caching;
using VideoStore.Handlers;
using VideoStore.Models;
using VideoStore.Repositories;

namespace VideoStore.Tests.Tests
{
    public class CreateMovieHandlerTest
    {
        [Test]
        public void New_Movie_Is_Successfully_Created_In_MovieRepository()
        {
            //Setup
            CreateMovieHandler createHandler;
            MovieCache cache;
            CreateHandlerAndCache(out createHandler, out cache);
            cache.Expire();
            
            //Act 
            var result = createHandler.CreateMovie(new Movie{ Title = "Return of the King"});
            Assert.That(result == 2);
        }

        [Test]
        public void New_Movie_Is_Inserted_Into_The_Cache()
        {
            //Setup
            CreateMovieHandler createHandler;
            MovieCache cache;
            CreateHandlerAndCache(out createHandler, out cache);

            //Act 
            createHandler.CreateMovie(new Movie { Title = "Return of the King" });

            Assert.That(cache.AllMovies().Count() == 2);
            Assert.That(cache.AllMovies().ElementAt(1).Title == "Return of the King");
        }

        private static void CreateHandlerAndCache(out CreateMovieHandler createHandler, out MovieCache cache)
        {
            var movieRespository = new Mock<IMovieRepository>();
            movieRespository.Setup(x => x.GetAllMovies()).Returns(new List<Movie> {new Movie()});
            movieRespository.Setup(x => x.CreateMovie(It.IsAny<Movie>())).Returns(2);
            
            cache = new MovieCache(movieRespository.Object);
            cache.Expire();
            createHandler = new CreateMovieHandler(movieRespository.Object, cache);
        }
    }
}
