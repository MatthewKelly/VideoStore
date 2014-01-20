using System.Collections.Generic;
using System.Linq;
using NUnit;
using NUnit.Framework;
using VideoStore.Controllers;
using VideoStore.Models;

namespace VideoStore.Tests.Controllers
{
    public class MovieControllerTest
    {
        [Test]
        public void Get_Returns_All_Results()
        {
            // Arrange
            var controller = new MoviesController();

            // Act
            var result = controller.Get(null, MovieAttribute.Classification);

            Assert.AreEqual(result.Count, 80);


        }

        [Test]
        public void Get_Returns_Results_With_Correct_Sorting_For_A_Number_Based_Field()
        {
            // Arrange0
            var controller = new MoviesController();

            // Act
            var result = controller.Get(null, MovieAttribute.MovieId).Results;
            
            Assert.IsTrue(result.ElementAt(0).MovieId <= result.ElementAt(1).MovieId);
            Assert.IsTrue(result.ElementAt(1).MovieId <= result.ElementAt(2).MovieId);
            Assert.IsTrue(result.ElementAt(2).MovieId <= result.ElementAt(3).MovieId);

        }


        [Test]
        public void Get_Returns_Results_With_Correct_Sorting_For_A_String_Based_Field()
        {
            // Arrange0
            var controller = new MoviesController();

            // Act
            var result = controller.Get(null, MovieAttribute.Genre).Results;

            Assert.IsTrue(result.ElementAt(0).Genre[0] <= result.ElementAt(1).Genre[0]);
            Assert.IsTrue(result.ElementAt(1).Genre[0] <= result.ElementAt(2).Genre[0]);
            Assert.IsTrue(result.ElementAt(2).Genre[0] <= result.ElementAt(3).Genre[0]);

        }

        [Test]
        public void Get_Returns_Results_With_Correct_Sorting_For_A_Date_Based_Field()
        {
            // Arrange0
            var controller = new MoviesController();

            // Act
            var result = controller.Get(null, MovieAttribute.ReleaseDate).Results;

            Assert.IsTrue(result.ElementAt(0).ReleaseDate <= result.ElementAt(1).ReleaseDate);
            Assert.IsTrue(result.ElementAt(1).ReleaseDate <= result.ElementAt(2).ReleaseDate);
            Assert.IsTrue(result.ElementAt(2).ReleaseDate <= result.ElementAt(3).ReleaseDate);

        }

        [Test]
        public void Search_Returns_Correct_Result()
        {
            // Arrange
            var controller = new MoviesController();

            // Act[
            var result = controller.Get(new SearchCriteria { Title = "The Day After Tomorrow" }).Results;

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.Single().Title, "The Day After Tomorrow");

        }
        
        /*
        [Test]
        public void Post()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [Test]
        public void Put()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }
    */
    }
    
}
 
