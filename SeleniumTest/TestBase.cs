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

        [SetUp]
        public void SetupTest()
        {
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            driver = new ChromeDriver();
            baseURL = "https://software-testing.ru/lms/my/";
            driver.Navigate().GoToUrl(baseURL);
            driver.Quit();
        }
    }
}
