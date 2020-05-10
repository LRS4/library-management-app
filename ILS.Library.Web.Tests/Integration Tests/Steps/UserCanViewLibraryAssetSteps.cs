using ILS.Library.Web.Tests.IntegrationTests.Core;
using ILS.Library.Web.Tests.IntegrationTests.Pages;
using ILS.Library.Web.Tests.IntegrationTests.Steps;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace ILS.Library.Web.Tests.IntegrationTests.Steps
{
    [Binding]
    public class UserCanViewLibraryAssetSteps : BaseBrowserSteps
    {
        #region Private members
        private CatalogDetailsPage _catalogDetailsPage;
        private CatalogPage _catalogPage;

        #endregion

        #region Constructor
        UserCanViewLibraryAssetSteps(BrowserDriver browserDriver) : base(browserDriver)
        {
            _catalogDetailsPage = new CatalogDetailsPage(BrowserDriver);
            _catalogPage = new CatalogPage(BrowserDriver);
        }
        #endregion

        #region Public methods
        [Given(@"User is at the catalogue page")]
        public void GivenUserIsAtTheCataloguePage()
        {
            _catalogPage.Open();
            var assetsTable = _catalogPage.AssetsTable;
            Assert.IsNotNull(assetsTable);
        }
        
        [Given(@"User wants to view Plato'(.*)'The Republic' book")]
        public void GivenUserWantsToViewPlatoTheRepublicBook(string p0)
        {
            var assetRows = _catalogPage.AssetRows;
            Assert.IsNotNull(assetRows);
        }
        
        [When(@"User clicks image of book cover")]
        public void WhenUserClicksImageOfBookCover()
        {
            var asset = _catalogPage.GetAsset("JaneEyre");

            Assert.IsNotNull(asset);
            asset.Click();
        }
        
        [Then(@"the book details page will display item details")]
        public void ThenTheBookDetailsPageWillDisplayItemDetails()
        {
            var title = _catalogDetailsPage.Title;

            Assert.AreEqual(title, "Jane Eyre");
        }

        #endregion
    }
}
