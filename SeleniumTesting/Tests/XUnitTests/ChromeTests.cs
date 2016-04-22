using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTesting.Tests.XUnitTests.TestFixtures;
using Xunit;

namespace SeleniumTesting.Tests.XUnitTests
{
    public class ChromeTests : ChromeFixture, IClassFixture<ChromeFixture>
    {
        private readonly ChromeDriver _driver;

        public ChromeTests()
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
            File.WriteAllText("chrome-source-1.html", _driver.PageSource);

            var query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium");

            var end = DateTime.Now.AddSeconds(5);
            while(DateTime.Now < end)
            {
                var resultsDiv = _driver.FindElement(By.ClassName("sbdd_b"));
                if(resultsDiv.Displayed)
                {
                    break;
                }
            }

            var allSuggestions = _driver.FindElements(By.XPath("//div[@class='sbqs_c']"));
            foreach(var suggestion in allSuggestions)
            {
                Console.WriteLine(suggestion.Text);
            }

            query.Submit();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.Title.StartsWith("Selenium"));

            Assert.Equal("Selenium - Google Search", _driver.Title);

            _driver.GetScreenshot().SaveAsFile("chrome-snapshot.png", ImageFormat.Png);
        }
    }
}
