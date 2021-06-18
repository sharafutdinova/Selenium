using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace HW
{
    internal class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
            catch
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
