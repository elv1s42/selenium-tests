using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing.Imaging;
using Xunit;

namespace SeleniumTesting
{
    public class FireFoxTests : IClassFixture<FireFoxFixture>
    {
        private FirefoxDriver _driver;

        public void SetFixture(FireFoxFixture data)
        {
            _driver = data.GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            _driver.Navigate().GoToUrl("http://www.google.com/ncr");
            var query = _driver.GetElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Title.StartsWith("Selenium"));

            Assert.Equal("Selenium - Google Search", _driver.Title);

            _driver.GetScreenshot().SaveAsFile("firefox-snapshot.png", ImageFormat.Png);
        }
    }

    public class FireFoxFixture : IDisposable
    {
        private readonly FirefoxDriver _driver;

        public FireFoxFixture()
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
