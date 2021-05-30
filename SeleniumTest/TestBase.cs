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
    [TestClass]
    public partial class TestBase
    {

        private IWebDriver driver;
        private string baseURL;

        [TestInitialize]
        public void BeforeTest()
        {
            //driver = new InternetExplorerDriver();
            driver = new ChromeDriver();
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Program Files\Firefox Nightly\firefox.exe";
            //driver = new FirefoxDriver();
        }   

        [TestMethod]
        public void LoginTest()
        {
            baseURL = "http://localhost/litecart/admin/login.php";
            OpenPage(baseURL);
            Login("admin", "admin");           
        }

        [TestMethod]
        public void OpenPage()
        {
            baseURL = "https://software-testing.ru/";
            OpenPage(baseURL);
        }

        [TestMethod]
        public void Menu()
        {
            LoginTest();
            GetMenu();
        }


        [TestCleanup]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
