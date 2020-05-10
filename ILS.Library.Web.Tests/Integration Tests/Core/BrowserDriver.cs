using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ILS.Library.Web.Tests.IntegrationTests.Core
{
    [ExcludeFromCodeCoverage]
    public class BrowserDriver
    {
        private WebDriverWait _browserWait;
        private IWebDriver _browser;

        public BrowserDriver() { }

        public bool IsInitialised
        {
            get
            {
                return (_browser != null);
            }
        }

        public IWebDriver Browser
        {
            get
            {
                if (_browser == null)
                {
                    throw new NullReferenceException(
                        "The WebDriver browser instance was not initialised. Call StartBrowser()");
                }

                return _browser;
            }
            set
            {
                _browser = value;
            }
        }

        public WebDriverWait BrowserWait
        {
            get
            {
                if (_browserWait == null || _browser == null)
                {
                    throw new NullReferenceException(
                        "The WebDriver browser wait instance was not initialised. Call StartBrowser()");
                }

                return _browserWait;
            }

            private set
            {
                _browserWait = value;
            }
        }

        public void StartBrowser()
        {
            var configBrowserType = TestContext.Parameters.GetAndCheckEnvironment("BrowserType");

            BrowserTypes browserType;

            if (!Enum.TryParse<BrowserTypes>(configBrowserType, out browserType) ||
                browserType == BrowserTypes.NotSet) 
            {
                // Default to Chrome
                browserType = BrowserTypes.Chrome;
            }

            StartBrowser(browserType);
        }

        public void StartBrowser(BrowserTypes browserType)
        {
            switch (browserType)
            {
                case BrowserTypes.Firefox:
                    var firefoxOptions = new FirefoxOptions();

                    Browser = new FirefoxDriver(
                        Environment.ExpandEnvironmentVariables(
                            TestContext.Parameters.GetAndCheckEnvironment("GeckoWebDriver")));
                    break;
                case BrowserTypes.InternetExplorer:
                    var ieOptions = new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true
                    };

                    Browser = new InternetExplorerDriver(
                        Environment.ExpandEnvironmentVariables(
                            TestContext.Parameters.GetAndCheckEnvironment("IEWebDriver")), ieOptions);
                    break;
                case BrowserTypes.Chrome:
                    var chromeOptions = new ChromeOptions();

                    Browser = new ChromeDriver("C:\\Users\\L.Spencer\\source\\repos\\ILS.Library\\ILS.Library.Web.Tests\\Integration Tests\\WebDrivers", chromeOptions);
                    break;
                case BrowserTypes.Edge:
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.UseInPrivateBrowsing = true;

                    Browser = new EdgeDriver(
                        Environment.ExpandEnvironmentVariables(
                            TestContext.Parameters.GetAndCheckEnvironment("EdgeWebDriver")), edgeOptions);
                    break;
                default:
                    break;
            }

            int.TryParse(TestContext.Parameters.GetAndCheckEnvironment("WaitTimeout"), out int waitTimeout);
            BrowserWait = new WebDriverWait(Browser, TimeSpan.FromSeconds(waitTimeout));
        }

        public void StopBrowser()
        {
            if (_browser != null)
            {
                Browser.Quit();
            }

            Browser = null;
            BrowserWait = null;
        }
    }
}
