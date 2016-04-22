using System;
using OpenQA.Selenium.Chrome;

namespace SeleniumTesting.Tests.XUnitTests.TestFixtures
{
    public class ChromeFixture : IDisposable
    {
        private readonly ChromeDriver _driver;

        public ChromeFixture()
        {
            _driver = new ChromeDriver();
        }

        public ChromeDriver GetDriver()
        {
            return _driver;
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}