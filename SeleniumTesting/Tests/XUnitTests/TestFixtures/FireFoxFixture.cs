using System;
using OpenQA.Selenium.Firefox;

namespace SeleniumTesting.Tests.XUnitTests.TestFixtures
{
    public class FirefoxFixture : IDisposable
    {
        private readonly FirefoxDriver _driver;

        public FirefoxFixture()
        {
            Environment.SetEnvironmentVariable("webdriver.log.file", "log-file.txt");
            Environment.SetEnvironmentVariable("webdriver.firefox.logfile", "ff-log.txt");
            _driver = new FirefoxDriver();
        }

        public FirefoxDriver GetDriver()
        {
            return _driver;
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}