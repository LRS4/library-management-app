using ILS.Library.Web.Tests.IntegrationTests.Core;
using OpenQA.Selenium;

namespace ILS.Library.Web.Tests.IntegrationTests.Pages
{
    public class CatalogDetailsPage : BasePage
    {
        private BrowserDriver _driver;

        public CatalogDetailsPage(BrowserDriver driver) : base(driver)
        { }

        public string Title
        {
            get
            {
                string title = null;

                try
                {
                    title = Driver.Browser.FindElement(By.Id("itemTitle")).Text;
                }
                catch (NoSuchElementException) 
                { }

                return title;
            }
        }
    }
}
