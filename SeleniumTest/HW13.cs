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
        public void HW13()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().Refresh();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            OpenPage("http://localhost/litecart/en/");
            for (int i = 0; i < 3; i++)
            {
                AddProduct();
                driver.FindElement(By.CssSelector("header a")).Click();
            }

            driver.FindElement(By.CssSelector("#cart a[class=link]")).Click();

            int count = driver.FindElements(By.CssSelector("#order_confirmation-wrapper tr")).Count;//init of counter
            while (IsElementNotPresented(By.CssSelector("#checkout-cart-wrapper em")))
            {
                if (IsElementPresented(By.CssSelector("li[class=shortcut]")))
                {
                    driver.FindElement(By.CssSelector("li[class=shortcut]")).Click();
                    driver.FindElement(By.CssSelector("button[value=Remove]")).Click();
                    wait.Until(d => d.FindElements(By.CssSelector("#order_confirmation-wrapper tr")).Count == count - 1);//checking that item is deleted
                    count = driver.FindElements(By.CssSelector("#order_confirmation-wrapper tr")).Count;//updating counter
                }
                else
                    driver.FindElement(By.CssSelector("button[value=Remove]")).Click();//removing of last item
            }            
        }

        public void AddProduct()
        {
            driver.FindElement(By.CssSelector("[class=content] a[class=link]")).Click();
            if (IsElementPresented(By.CssSelector("[class=options] select")))
            {
                driver.FindElement(By.CssSelector("[class=options] select")).Click();
                driver.FindElement(By.CssSelector("[class=options] select option[value=Medium]")).Click();
            }
            IWebElement count = driver.FindElement(By.CssSelector("#cart-wrapper span[class=quantity]"));
            string num = (Convert.ToInt64(count.Text) + 1).ToString();

            driver.FindElement(By.CssSelector("[class=quantity] button")).Click();
            wait.Until(d => d.FindElement(By.CssSelector("#cart-wrapper span[class=quantity]")).Text == num);

        }
        public bool IsElementNotPresented(By locator)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0));
            try
            {
                driver.FindElement(locator);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                return false;
            }
            catch (NoSuchElementException)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                return true;
            }
        }
        public bool IsElementPresented(By locator)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
