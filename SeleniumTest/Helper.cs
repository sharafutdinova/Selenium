using System;
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

        public void Firefox()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            service.FirefoxBinaryPath = @"C:\Program Files\Firefox Nightly\firefox.exe";
        }

        public void OpenPage(string address)
        {
            driver.Navigate().GoToUrl(address);
        }

        public void Login(string login, string password)
        {
            try
            {
                driver.FindElement(By.Name("username")).SendKeys(login);
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.Name("login")).Click();
            }
            catch { throw new KeyNotFoundException("Element not found"); }
        }

        public void GetMenu()
        {
            IWebElement a = driver.FindElement(By.Id("box-apps-menu-wrapper"));
            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.Id("app-"));
            IReadOnlyCollection<IWebElement> sublist;
            int count = list.Count;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            for (int i = 0; i < count; i++)
            {
                list.ElementAt(i).Click();
                sublist = driver.FindElements(By.CssSelector("li#app-.selected li"));
                try
                {
                    driver.FindElement(By.CssSelector("td#content h1"));
                }
                catch (ElementNotInteractableException) { throw new Exception("No header"); }
                
                for (int j = 0; j< sublist.Count; j++)
                {
                    sublist.ElementAt(j).Click();
                    sublist = driver.FindElements(By.CssSelector("li#app-.selected li"));
                    try
                    {
                        driver.FindElement(By.CssSelector("td#content h1"));
                    }
                    catch (ElementNotInteractableException) { throw new Exception("No header"); }

                }
                list = driver.FindElements(By.Id("app-"));
            }
        }

        public void CheckSticsCount()
        {
            int count = 0;
            IReadOnlyCollection<IWebElement> productsList = driver.FindElements(By.CssSelector(".product.column.shadow.hover-light"));
            foreach (IWebElement e in productsList)
            {
                try
                {
                    count = e.FindElements(By.CssSelector(".sticker")).Count();
                    NUnit.Framework.Assert.AreEqual(count, 1);
                }
                catch (NoSuchElementException) { throw new NoSuchElementException(); }
            }
        }

        public int ElementsCount(By locator)
        {            
            return driver.FindElements(locator).Count;
        }

        bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }
        // явное ожидание появления элемента
        //WebdriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Name("q")));

        // настройка неявных ожиданий
        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //IWebElement element = driver.FindElement(By.Name("q"));

    }
}
