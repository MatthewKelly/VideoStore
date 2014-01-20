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
            MoviesController controller = new MoviesController();

            // Act
            var result = controller.Get(MovieAttributes.Classification);

            Assert.AreEqual(result.Count(), 80);


        }
        /*
        [TestMethod]
        public void GetById()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            controller.Delete(5);

            // Assert
        }
         */
    }
    
}
 
