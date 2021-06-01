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
        [TestMethod]
        public void NewUser()
        {
            var rand = new Random();
            driver = new ChromeDriver();
            OpenPage("http://localhost/litecart/en/");
            string email = rand.Next().ToString() + "@mail.ru";
            CreateAccount(email, "1234");

            driver.FindElement(By.PartialLinkText("Logout")).Click();
            LoginWithNewUser(email, "1234");
            driver.FindElement(By.PartialLinkText("Logout")).Click();
        }

        public void CreateAccount(string email, string pass)
        {
            driver.FindElement(By.CssSelector("tbody a")).Click();
            driver.FindElement(By.CssSelector("[name=firstname]")).SendKeys("Fname");
            driver.FindElement(By.CssSelector("[name=lastname]")).SendKeys("Lname");
            driver.FindElement(By.CssSelector("[name=address1]")).SendKeys("address1");
            driver.FindElement(By.CssSelector("[name=postcode]")).SendKeys("12345");
            driver.FindElement(By.CssSelector("[name=city]")).SendKeys("City");
            driver.FindElement(By.CssSelector("[name=country_code]")).SendKeys("United States" + Keys.Enter);
            driver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("[name=phone]")).SendKeys("+12345");
            driver.FindElement(By.CssSelector("[name=password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(pass);             
            driver.FindElement(By.CssSelector("[name=create_account]")).Click();

            driver.FindElement(By.CssSelector("[name=country_code]")).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector("[name=zone_code]")).SendKeys("Alaska");
            driver.FindElement(By.CssSelector("[name=password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name=create_account]")).Click();
        }

        public void LoginWithNewUser(string email, string pass)
        {
            driver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("[name=password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name=login]")).Click();
        }
    }
}
