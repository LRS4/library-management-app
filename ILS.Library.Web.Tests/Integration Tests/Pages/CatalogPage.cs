using ILS.Library.Web.Tests.IntegrationTests.Core;
using ILS.Library.Web.Tests.IntegrationTests.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILS.Library.Web.Tests.IntegrationTests.Pages
{
    public class CatalogPage : BasePage
    {
        public CatalogPage(BrowserDriver driver) : base(driver) { }

        public override void Open(string part = "Catalog")
        {
            base.Open(part);
        }

        public IWebElement AssetsTable
        {
            get
            {
                IWebElement element = null;

                try
                {
                    element = Driver.Browser.FindElement(By.Id("assetsTable"));
                }
                catch (NoSuchElementException) 
                { }

                return element;
            }
        }

        public ICollection<IWebElement> AssetRows
        {
            get
            {
                ICollection<IWebElement> elements = null;

                try
                {
                    elements = Driver.Browser.FindElements(By.ClassName("assetRow"));
                }
                catch
                { }

                return elements;
            }
        }

        public IWebElement GetAsset(string idAssetSelector)
        {
            var element = Driver.Browser.FindElement(By.Id(idAssetSelector));
            return element;
        }
    }
}
