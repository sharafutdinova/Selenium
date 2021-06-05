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
        private WebDriverWait wait;

        [TestInitialize]
        public void BeforeTest()
        {
            //driver = new InternetExplorerDriver();
            //driver = new ChromeDriver();
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            //service.FirefoxBinaryPath = @"C:\Program Files\Firefox Nightly\firefox.exe";
            //driver = new FirefoxDriver();
            //LoginTest();
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
            GetMenu();
        }

        [TestMethod]
        public void CheckStics()
        {
            driver = new ChromeDriver();
            OpenPage("http://localhost/litecart/en/");
            CheckSticsCount();
        }

        [TestMethod]
        public void CheckCountriesOrder()
        {
            CheckOrderOfCountries();
            CheckOrderOfSubCountries();
        }

        [TestMethod]
        public void CheckZonesOrder()
        {
            CheckOrderOfGeoZones();
        }

        [TestMethod]
        public void Campaigns()
        {
            driver = new ChromeDriver();
            CheckItemPage();
            driver.Quit();
            driver = new FirefoxDriver();
            CheckItemPage();
            driver.Quit();
            driver = new InternetExplorerDriver();
            CheckItemPage();
            driver.Quit();
        }


        [TestCleanup]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
