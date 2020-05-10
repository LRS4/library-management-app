using ILS.Library.Web.Tests.IntegrationTests.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILS.Library.Web.Tests.IntegrationTests.Pages
{
    public abstract class BasePage
    {
        protected readonly BrowserDriver Driver;

        public BasePage(BrowserDriver driver)
        {
            Driver = driver;
        }

        public virtual string Url
        {
            get
            {
                return "https://localhost:44362/";
            }
        }

        public virtual void Open(string part = "")
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new ArgumentException("The main URL cannot be null or empty");
            }
            Driver.Browser.Navigate().GoToUrl(string.Concat(Url, part));
        }
    }
}
