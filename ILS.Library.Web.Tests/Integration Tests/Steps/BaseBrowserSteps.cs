using Gherkin.Pickles;
using ILS.Library.Web.Tests.IntegrationTests.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TechTalk.SpecFlow;

namespace ILS.Library.Web.Tests.IntegrationTests.Steps
{
    [ExcludeFromCodeCoverage]
    public class BaseBrowserSteps
    {
        private readonly BrowserDriver _browserDriver;

        protected BaseBrowserSteps(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver ??
                throw new ArgumentNullException(
                    nameof(browserDriver));
        }

        protected BrowserDriver BrowserDriver
        {
            get
            {
                return _browserDriver;
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (!_browserDriver.IsInitialised)
            {
                _browserDriver.StartBrowser();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _browserDriver.StopBrowser();
        }
    }
}
