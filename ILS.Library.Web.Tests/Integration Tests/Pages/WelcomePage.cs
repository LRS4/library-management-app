using ILS.Library.Web.Tests.IntegrationTests.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILS.Library.Web.Tests.IntegrationTests.Pages
{
    public class WelcomePage : BasePage
    {
        public WelcomePage(BrowserDriver driver) : base(driver) { }

        public override string Url
        {
            get
            {
                return "https://localhost:44362/";
            }
        }

        public IWebElement StartButton
        {
            get
            {
                IWebElement element = null;

                try
                {
                    element = Driver.Browser.FindElement(
                        By.Id("startNow"));
                }
                catch (NoSuchElementException)
                { }

                return element;
            }
        }
    }
}
