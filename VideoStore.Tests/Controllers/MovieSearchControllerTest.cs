using System.Linq;
using NUnit.Framework;
using VideoStore.Models;
using VideoStore.Controllers;

namespace VideoStore.Tests.Controllers
{
    public class MovieSearchControllerTest
    {
        [Test]
        public void Search_Returns_Correct_Result()
        {
            // Arrange
            var controller = new MovieSearchController();

            // Act[
            var result = controller.Get(new SearchCriteria{ Title = "The Day After Tomorrow"});

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.Single().Title, "The Day After Tomorrow");

        }
      
    }

}

