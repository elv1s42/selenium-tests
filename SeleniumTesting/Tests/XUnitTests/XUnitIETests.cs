using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumTesting.Tests.XUnitTests.TestFixtures;
using Xunit;

namespace SeleniumTesting.Tests.XUnitTests
{
    public class XUnitIETests : IEFixture, IClassFixture<IEFixture>
    {
        private readonly InternetExplorerDriver _driver;

        public XUnitIETests()
        {
            _driver = GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            _driver.Navigate().GoToUrl("http://www.google.com/ncr");
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            // here you can check HTML of the page you currently have loaded in the browser
            // and save it to the file
            File.WriteAllText("ie-source-1.html", _driver.PageSource);

            var query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.Title.StartsWith("Selenium"));

            Assert.Equal("Selenium - Google Search", _driver.Title);

            _driver.GetScreenshot().SaveAsFile("ie-snapshot.png", ImageFormat.Png);
        }
    }
}
