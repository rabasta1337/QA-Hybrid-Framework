using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.UI.Pages
{
    internal class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement CheckoutButton => _driver.FindElement(By.Id("checkout"));

        public void ClickCheckout()
        {
            CheckoutButton.Click();
        }
    }
}
