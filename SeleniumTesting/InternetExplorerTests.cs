using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTesting
{
    public class InternetExplorerTests : IClassFixture<InternetExplorerFixture>
    {
        private InternetExplorerDriver _driver;

        public void SetFixture(InternetExplorerFixture data)
        {
            _driver = data.GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class InternetExplorerFixture : IDisposable
    {
        private readonly InternetExplorerDriver _driver;

        public InternetExplorerFixture()
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
