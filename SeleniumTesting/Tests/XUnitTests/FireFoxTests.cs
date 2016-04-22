using System;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumTesting.Tests.XUnitTests.TestFixtures;
using Xunit;

namespace SeleniumTesting.Tests.XUnitTests
{
    public class FirefoxTests : IClassFixture<FirefoxFixture>
    {
        private FirefoxDriver _driver;

        public void SetFixture(FirefoxFixture data)
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
}
