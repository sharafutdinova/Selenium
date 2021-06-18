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
    public class Application
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ProductPage productPage;
        private BasketPage basketPage;
        private MainPage mainPage;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            productPage = new ProductPage(driver);
            basketPage = new BasketPage(driver);
            mainPage = new MainPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        public void OpenPage(string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void AddProduct()
        {
            mainPage.OpenFirstProductPage();
            productPage.SetSizeOfItem();
            long numBefore = productPage.GetCountOfProductsInBasket();
            productPage.AddProductToBasket();
            wait.Until(d => numBefore + 1 == productPage.GetCountOfProductsInBasket());
        }

        public void DeleteProducts()
        {
            int count = basketPage.GetCountOfItemsInBasket();
            while (basketPage.IsElementNotPresented(basketPage.EmptyBasketTitle()))
            {
                if (basketPage.IsElementPresented(basketPage.OpenProductButton()))
                {
                    basketPage.SelectProduct();
                    basketPage.DeleteProductFromBasket();
                    wait.Until(d => count == basketPage.GetCountOfItemsInBasket() + 1);
                    count = basketPage.GetCountOfItemsInBasket();
                }
                else
                    basketPage.DeleteProductFromBasket();
            }
        }

        public void AddAndDeleteProducts()
        {
            OpenPage("http://localhost/litecart/en/");
            for (int i = 0; i < 3; i++)
            {
                AddProduct();
                mainPage.OpenPage();
            }
            basketPage.OpenBasket();

            DeleteProducts();
        }
    }
}
