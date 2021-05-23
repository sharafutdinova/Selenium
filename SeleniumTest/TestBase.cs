using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;


namespace SeleniumTest
{
    [TestClass]
    public class TestBase
    {

        private IWebDriver driver;
        private string baseURL;

        [TestInitialize]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
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

        [TestMethod]
        public void LoginTest()
        {
            baseURL = "http://localhost/litecard/admin/login.php";
            OpenPage(baseURL);
            Login("admin", "admin");           
        }

        [TestMethod]
        public void OpenPage()
        {
            baseURL = "https://software-testing.ru/";
            OpenPage(baseURL);
        }

        [TestCleanup]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
