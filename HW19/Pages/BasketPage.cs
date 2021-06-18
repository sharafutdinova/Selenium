using System;
using OpenQA.Selenium;

namespace HW
{
    internal class BasketPage : Page
    {
        public BasketPage(IWebDriver driver) : base(driver) { }

        internal void OpenBasket()
        {
            driver.FindElement(By.CssSelector("#cart a[class=link]")).Click();
        }

        internal int GetCountOfItemsInBasket()
        {
            return driver.FindElements(By.CssSelector("#order_confirmation-wrapper tr")).Count;
        }

        internal void SelectProduct()
        {
            driver.FindElement(By.CssSelector("li[class=shortcut]")).Click();
        }

        internal void DeleteProductFromBasket()
        {
            driver.FindElement(By.CssSelector("button[value=Remove]")).Click();
        }

        internal By EmptyBasketTitle()
        {
            return By.CssSelector("#checkout-cart-wrapper em");
        }

        internal By OpenProductButton()
        {
            return By.CssSelector("li[class=shortcut]");
        }
    }
}
