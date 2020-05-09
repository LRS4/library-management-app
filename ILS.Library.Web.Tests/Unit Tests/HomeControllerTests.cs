using ILS.Library.Web.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ILS.Library.Web.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test(Description = "Verifies that the default view (Index) for the HomeController returns" +
            "the landing page if the user is not authenticated.")]
        public void IndexReturnsDefaultView()
        {
            // Arrange
            var logger = Substitute.For<ILogger<HomeController>>();
            var homeController = new HomeController(logger);

            // Act
            var actionResult = homeController.Index();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
        } 
    }
}
