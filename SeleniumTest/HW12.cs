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
        public void HW12()
        {
            driver = new ChromeDriver();
            OpenPage("http://localhost/litecart/admin/");
            LoginTest();
            driver.FindElement(By.CssSelector("#box-apps-menu a[href*=catalog]")).Click();
            Thread.Sleep(2000);
            int count = driver.FindElements(By.CssSelector("table[class=dataTable] tr[class=row]")).Count;
            driver.FindElement(By.CssSelector("a[class=button][href*=edit_product]")).Click();
            Thread.Sleep(2000);

            GeneralTab();
            InfoTab();
            PricesTab();
            driver.FindElement(By.CssSelector("button[name=save]")).Click();
            Thread.Sleep(2000);
            int newCount = driver.FindElements(By.CssSelector("table[class=dataTable] tr[class=row]")).Count;
            NUnit.Framework.Assert.AreEqual(newCount - 1, count);
        }

        public void GeneralTab()
        {
            driver.FindElement(By.CssSelector("input[type=checkbox][name*=product_groups][value*='2']")).Click();
            driver.FindElement(By.CssSelector("input[type=radio][name=status]")).Click();
            var rand = new Random();
            driver.FindElement(By.CssSelector("input[name*=name]")).SendKeys("Test product");
            driver.FindElement(By.CssSelector("input[name=code]")).SendKeys(rand.Next().ToString());
            driver.FindElement(By.CssSelector("input[type=checkbox][data-name=Subcategory]")).Click();
            driver.FindElement(By.CssSelector("input[name=quantity]")).SendKeys(Keys.Delete + "10");

            try
            {
                string fileName = "1.jpg";
                string path = Path.GetFullPath(fileName);
                driver.FindElement(By.CssSelector("input[name*=images]")).SendKeys(path);
            }
            catch { throw new FileNotFoundException(); }
            driver.FindElement(By.CssSelector("input[name=date_valid_from]")).SendKeys("10.10.2020");
            driver.FindElement(By.CssSelector("input[name=date_valid_to]")).SendKeys("10.10.2030");
        }

        public void InfoTab()
        {
            driver.FindElement(By.CssSelector("a[href*=tab-information]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("[name=manufacturer_id]")).Click();
            driver.FindElement(By.CssSelector("[name=manufacturer_id] [value='1']")).Click();
            driver.FindElement(By.CssSelector("[name=keywords]")).SendKeys("Keywords");
            driver.FindElement(By.CssSelector("[name*=short_description]")).SendKeys("Short descr");
            driver.FindElement(By.CssSelector("[contenteditable=true]")).SendKeys("Long description");

            driver.FindElement(By.CssSelector("[name*=head_title]")).SendKeys("H title");
            driver.FindElement(By.CssSelector("[name*=meta_description]")).SendKeys("M descr");
        }

        public void PricesTab()
        {
            driver.FindElement(By.CssSelector("a[href*=tab-prices]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("[name=purchase_price]")).SendKeys(Keys.Delete + "12");
            driver.FindElement(By.CssSelector("[name=purchase_price_currency_code]")).Click();
            driver.FindElement(By.CssSelector("[name=purchase_price_currency_code] [value=EUR]")).Click();
            driver.FindElement(By.CssSelector("tbody input[type=text][name*=USD]")).SendKeys(Keys.Clear + "12");
            driver.FindElement(By.CssSelector("tbody input[type=text][name*=EUR]")).SendKeys(Keys.Clear + "12");
        }
    }
}
