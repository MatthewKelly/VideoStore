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
    public class UpdateMovieHandlerTest
    {
        [Test]
        public void Updated_Movie_Is_Updated_In_The_Movie_Repository()
        {
            //Setup
            var repositoryCalled = false;
            var originalMovie = new Movie { MovieId = 1, Title = "Return of the King" };
            var updatedMovie = new Movie { MovieId = 1, Title = "Return of the King 2" };

            var movieRespository = new Mock<IMovieRepository>();
            movieRespository.Setup(x => x.GetAllMovies()).Returns(new List<Movie> { originalMovie });
            movieRespository.Setup(x => x.UpdateMovie(It.Is<Movie>(y => y == updatedMovie))).Callback(() => repositoryCalled = true);
            
            var cache = new MovieCache(movieRespository.Object);
            cache.Expire();
            var updateHandler = new UpdateMovieHandler(movieRespository.Object, cache);

            //Act 
            updateHandler.UpdateMovie(updatedMovie);

            Assert.That(repositoryCalled);
        }

        [Test]
        public void Updated_Movie_Is_Updated_In_The_Cache()
        {
            //Setup
            var originalMovie = new Movie { MovieId = 1, Title = "Return of the King" };
            var updatedMovie = new Movie { MovieId = 1, Title = "Return of the King 2" };

            MovieCache cache;
            var movieRespository = CreateMovieRepositoryAndCache(originalMovie, out cache);
            var updateHandler = new UpdateMovieHandler(movieRespository.Object, cache);
            

            //Act 
            updateHandler.UpdateMovie(updatedMovie);

            Assert.That(cache.AllMovies().Single().Title == updatedMovie.Title);
        }

        [Test]
        public void Exception_Thrown_If_Updated_Movie_Does_Not_Exist()
        {
            //Setup
            var originalMovie = new Movie { MovieId = 1, Title = "Return of the King" };
            var updatedMovie = new Movie { MovieId = 2, Title = "Return of the King 2" };

            MovieCache cache;
            var movieRespository = CreateMovieRepositoryAndCache(originalMovie, out cache);
            var updateHandler = new UpdateMovieHandler(movieRespository.Object, cache);


            //Act 
            Assert.Throws(typeof (InvalidOperationException), () => updateHandler.UpdateMovie(updatedMovie));
        }

        private Mock<IMovieRepository> CreateMovieRepositoryAndCache(Movie originalMovie, out MovieCache cache)
        {
            var movieRespository = new Mock<IMovieRepository>();
            movieRespository.Setup(x => x.GetAllMovies()).Returns(new List<Movie> { originalMovie });

            cache = new MovieCache(movieRespository.Object);
            cache.Expire();
            return movieRespository;
        }


        
    }
}
