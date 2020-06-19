using ILS.Library.Web.Controllers;
using ILS.Library.Web.Models.Catalog;
using ILS.Library.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILS.Library.Web.Tests
{
    [TextFixture]
    class CatalogControllerTests
    {
        [Test(Description = "Verifies that the default view (Index) for the CatalogController returns" +
            "the catalog page.")]
        public void IndexReturnsCatalogListView()
        {
            // Arrange
            var libraryAssetService = Substitute.For<ILibraryAssetService>();
            var checkoutService = Substitute.For<ICheckoutService>();
            var branchService = Substitute.For<IBranchService>();
            var catalogController = new CatalogController(libraryAssetService, checkoutService, branchService);

            // Act
            var actionResult = catalogController.Index();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var viewResult = actionResult as ViewResult;

            Assert.That(viewResult.Model, Is.InstanceOf<IndexViewModel>());
        }
    }
}
