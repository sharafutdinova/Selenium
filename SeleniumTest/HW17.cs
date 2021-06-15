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
using OpenQA.Selenium.Support.Events;
using NUnit.Framework;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Remote;
using System.Reflection;


namespace SeleniumTest
{
    public partial class TestBase
    {
        [TestMethod]
        public void HW17()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new ChromeDriver(options);

            OpenPage("http://localhost/litecart/admin/");
            LoginTest();
            IReadOnlyCollection<LogEntry> logs;
            OpenPage("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("[class=row] [href*='category_id=1&product_id=']"));
            
            List<string> items = new List<string>();
            for (int i = 0; i < list.Count; i= i+2)
            {
                items.Add(list.ElementAt(i).GetAttribute("href"));
            }
            
            foreach (string item in items)
            {
                driver.FindElement(By.CssSelector($"[href='{item}']")).Click();

                logs = driver.Manage().Logs.GetLog("browser");
                try
                {
                    NUnit.Framework.Assert.IsEmpty(logs);
                }
                catch
                {
                    foreach (LogEntry l in logs)
                    {
                        Console.WriteLine(l);
                    }
                    throw new Exception("Browser has logs");
                }
                OpenPage("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            }
        }
        
    }
}
