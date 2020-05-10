using ILS.Library.Web.Tests.IntegrationTests.Core;
using ILS.Library.Web.Tests.IntegrationTests.Pages;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;

namespace ILS.Library.Web.Tests.IntegrationTests.Steps
{
    [Binding]
    [ExcludeFromCodeCoverage]
    public class UserCanViewLibraryCatalogueSteps : BaseBrowserSteps
    {
        #region Private members
        private WelcomePage _welcomePage;
        private CatalogPage _catalogPage;

        #endregion

        #region Constructor
        public UserCanViewLibraryCatalogueSteps(BrowserDriver browserDriver) : base(browserDriver) 
        {
            _welcomePage = new WelcomePage(BrowserDriver);
            _catalogPage = new CatalogPage(BrowserDriver);
        }

        #endregion

        #region Public methods
        [Given(@"User is at the landing page")]
        public void GivenUserIsAtTheLandingPage()
        {
            _welcomePage.Open();
        }
        
        [Given(@"User clicks the start now button")]
        public void GivenUserClicksTheStartNowButton()
        {
            var startButton = _welcomePage.StartButton;
            startButton.Click();

            Assert.IsNotNull(startButton);
        }
        
        [When(@"User is at library catalogue page")]
        public void WhenUserIsAtLibraryCataloguePage()
        {
            var assetsTable = _catalogPage.AssetsTable;

            Assert.IsNotNull(assetsTable);
        }
        
        [Then(@"the catalogue should be displayed on the screen")]
        public void ThenTheCatalogueShouldBeDisplayedOnTheScreen()
        {
            var assetsTable = _catalogPage.AssetsTable;
            var assetRows = _catalogPage.AssetRows;

            Assert.IsNotNull(assetsTable);
            Assert.That(assetRows.Count > 0);
        }
        #endregion
    }
}
