using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTesting
{
    public static class WebDriverExtensions
    {
        public static SelectElement GetSelectElement(this IWebDriver driver, By by)
        {
            return new SelectElement(driver.GetElement(by));
        }

        public static IWebElement GetElement(this IWebDriver driver, By by)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    return driver.FindElement(by);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception was raised on locating element: " + e.Message);
                }
            }
            throw new ElementNotVisibleException(by.ToString());
        }
    }
}
