using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.UI.Pages
{
    internal class InventoryPage
    {
        private readonly IWebDriver _driver;

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement AddBackpackButton => _driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
        private IWebElement CartCounter => _driver.FindElement(By.ClassName("shopping_cart_badge"));

        public void AddBackpackToCart()
        {
            AddBackpackButton.Click();
        }

        public string GetCartBadgeText()
        {
            return CartCounter.Text;
        }
    }
}
