using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace SeleniumTest
{
    public partial class TestBase
    {
        [TestMethod]
        public void HW14()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().Refresh();
            OpenPage("http://localhost/litecart/admin/");
            LoginTest();
            OpenPage("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.FindElement(By.CssSelector("a[title=Edit]")).Click();
            IReadOnlyCollection<IWebElement> links = driver.FindElements(By.CssSelector("#content tbody a[target=_blank]"));
            string currentWindow = driver.CurrentWindowHandle;
            string newWindow;
            IReadOnlyCollection<string> windows = driver.WindowHandles;
            foreach (IWebElement link in links)
            {
                link.Click();
                wait.Until(d => d.WindowHandles.Count == windows.Count + 1);
                newWindow = NewWindow(currentWindow);
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(currentWindow);
            }
        }

        public string NewWindow(string currentWindow)
        {
            IReadOnlyCollection<string> newWindows = driver.WindowHandles;
            int i = 0;
            while (newWindows.ElementAt(i)==currentWindow)
            {
                i++;
            }
            return newWindows.ElementAt(i);
        }
    }
}
