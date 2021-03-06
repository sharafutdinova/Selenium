using System;
using OpenQA.Selenium;

namespace HW
{
    internal class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver) { }
        
        internal void SetSizeOfItem()
        {
            if (IsElementPresented(By.CssSelector("[class=options] select")))
            {
                driver.FindElement(By.CssSelector("[class=options] select")).Click();
                driver.FindElement(By.CssSelector("[class=options] select option[value=Medium]")).Click();
            }
        }

        internal void AddProductToBasket()
        {
            driver.FindElement(By.CssSelector("[class=quantity] button")).Click();
        }

        internal long GetCountOfProductsInBasket()
        {
            IWebElement count = driver.FindElement(By.CssSelector("#cart-wrapper span[class=quantity]"));
            return Convert.ToInt64(count.Text);
        }

    }
}
