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
    [TestClass]
    public class AddingAndDeletingProductTest : TestBase
    {
        [TestMethod]
        public void AddAndDeleteProduct()
        {
            app.AddAndDeleteProducts();
        }
    }
}
