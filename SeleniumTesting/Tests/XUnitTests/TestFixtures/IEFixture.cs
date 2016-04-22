using System;
using OpenQA.Selenium.IE;

namespace SeleniumTesting.Tests.XUnitTests.TestFixtures
{
    public class IEFixture : IDisposable
    {
        private readonly InternetExplorerDriver _driver;

        public IEFixture()
        {
            var service = InternetExplorerDriverService.CreateDefaultService();
            service.LogFile = "ie-log.txt";
            service.LoggingLevel = InternetExplorerDriverLogLevel.Debug;

            _driver = new InternetExplorerDriver(service, new InternetExplorerOptions
            {
                IgnoreZoomLevel = true
            });
        }

        public InternetExplorerDriver GetDriver()
        {
            return _driver;
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}