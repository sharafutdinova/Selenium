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

        public void CheckItemPage()
        {
            OpenPage("http://localhost/litecart/en/");
            IWebElement first = driver.FindElement(By.CssSelector("#box-campaigns .product"));
            string name = first.FindElement(By.ClassName("name")).Text;
            IWebElement cprice = first.FindElement(By.CssSelector("strong.campaign-price"));
            IWebElement rprice = first.FindElement(By.CssSelector("s.regular-price"));
            decimal rpriceSize = Convert.ToDecimal(rprice.GetCssValue("font-size").Replace("px", "").Replace(".", ","));
            decimal cpriceSize = Convert.ToDecimal(cprice.GetCssValue("font-size").Replace("px", "").Replace(".", ","));
            NUnit.Framework.Assert.IsTrue(cpriceSize>rpriceSize);
            NUnit.Framework.Assert.IsTrue(CheckPricesColor(cprice, rprice));
            string cprice1 = cprice.Text;
            string rprice1 = rprice.Text;

            first.Click();
            Thread.Sleep(2000);
            first = driver.FindElement(By.CssSelector("#box-product"));
            string name2 = first.FindElement(By.CssSelector("h1.title")).Text;
            cprice = first.FindElement(By.CssSelector("strong.campaign-price"));
            rprice = first.FindElement(By.CssSelector("s.regular-price"));
            NUnit.Framework.Assert.IsTrue(CheckPricesColor(cprice, rprice));
            rpriceSize = Convert.ToDecimal(rprice.GetCssValue("font-size").Replace("px", "").Replace(".", ","));
            cpriceSize = Convert.ToDecimal(cprice.GetCssValue("font-size").Replace("px", "").Replace(".", ","));
            NUnit.Framework.Assert.IsTrue(cpriceSize > rpriceSize);


            NUnit.Framework.Assert.AreEqual(name, name2);
            NUnit.Framework.Assert.AreEqual(cprice1, cprice.Text);
            NUnit.Framework.Assert.AreEqual(rprice1, rprice.Text);
        }

        public bool CheckPricesColor(IWebElement price1, IWebElement price2)
        {
            string colorCPrice = price1.GetCssValue("color").Replace(" ", "").Replace("rgb", "").Replace("(", "").Replace("a", "").Replace(")", "");
            string colorRPrice = price2.GetCssValue("color").Replace(" ", "").Replace("rgb", "").Replace("(", "").Replace("a", "").Replace(")", "");
            if (colorCPrice.Split(',')[1] == "0" && colorCPrice.Split(',')[2] == "0")
            {
                if (colorRPrice.Split(',')[0] == colorRPrice.Split(',')[1] && colorRPrice.Split(',')[1] == colorRPrice.Split(',')[2])
                {
                        return true;                    
                }
                else { return false; }
            }
            else { return false; }
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
            IReadOnlyCollection<IWebElement> productsList = driver.FindElements(By.CssSelector(".product"));//column.shadow.hover-light
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

        public void CheckOrderOfCountries()
        {
            OpenPage("http://localhost/litecart/admin/?app=countries&doc=countries");
            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("table.dataTable tr.row a"));
            List<string> namesList = new List<string>();
            string name;
            for (int i = 0; i < list.Count; i = i + 2)
            {
                name = list.ElementAt(i).GetAttribute("text");
                namesList.Add(name);
            }

            NUnit.Framework.Assert.IsTrue(CheckStringListOrder(namesList));
        }

        public void CheckOrderOfSubCountries()
        {
            OpenPage("http://localhost/litecart/admin/?app=countries&doc=countries");
            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("table.dataTable tr.row td"));
            IReadOnlyCollection<IWebElement> subCountries;
            for (int i = 5; i < list.Count; i = i + 7)
            {
                var s = list.ElementAt(i).GetAttribute("textContent");
                if (s!="0")
                {
                    list.ElementAt(i + 1).Click();
                    List<string> names = new List<string>();
                    subCountries = driver.FindElements(By.CssSelector("table.dataTable [type=hidden][name*=name]"));
                    for (int j = 0; j < subCountries.Count; j++)
                    {
                        names.Add(subCountries.ElementAt(0).GetAttribute("value"));
                    }
                    CheckStringListOrder(names);
                    OpenPage("http://localhost/litecart/admin/?app=countries&doc=countries");
                    list = driver.FindElements(By.CssSelector("table.dataTable tr.row td"));
                }
            }
            
        }

        public void CheckOrderOfGeoZones()
        {
            OpenPage("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("table.dataTable tr.row a"));
            IReadOnlyCollection<IWebElement> zones;
            for (int i = 0; i < list.Count; i = i + 2)
            {
                list.ElementAt(i).Click();
                List<string> zonesName = new List<string>();
                zones = driver.FindElements(By.CssSelector("[selected=selected]"));
                for (int j = 1; j < zones.Count; j = j + 2)
                {
                    zonesName.Add(zones.ElementAt(j).GetAttribute("text"));
                }
                NUnit.Framework.Assert.IsTrue(CheckStringListOrder(zonesName));
                
                OpenPage("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
                list = driver.FindElements(By.CssSelector("table.dataTable tr.row a"));
            }
            
        }

        public bool CheckStringListOrder(List<string> list)
        {
            List<string> orderedList = list.OrderBy(n => n).ToList();
            bool orderIsCorrect = true;
            for (int i = 0; i < orderedList.Count; i++)
            {
                if (list[i] != orderedList[i])
                {
                    orderIsCorrect = false;
                }
            }
            return orderIsCorrect;
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
