using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private IWebElement CartIcon => _driver.FindElement(By.ClassName("shopping_cart_link"));
        private IWebElement SortContainer => _driver.FindElement(By.ClassName("product_sort_container"));
       

        public void AddBackpackToCart()
        {
            AddBackpackButton.Click();
        }

        public string GetCartBadgeText()
        {
            return CartCounter.Text;
        }

        public void GoToCart()
        {
            CartIcon.Click();
        }

        public void SortProducts(string OptionValue)
        {
            var select = new SelectElement(SortContainer);
            select.SelectByValue(OptionValue);
        }


        public List<decimal> GetItemPrices()
        {
            var priceElements = _driver.FindElements(By.ClassName("inventory_item_price"));
            var prices = new List<decimal>();

            foreach (var element in priceElements)
            {
                string cleanText = element.Text.Replace("$", "").Trim();

                // Bezpieczne parsowanie z uwzględnieniem kropki (InvariantCulture) i w locie przerzucenie do zmiennej price bo samo tryparse to bool
                if (decimal.TryParse(cleanText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal price))
                {
                    prices.Add(price);
                }


            }

            return prices;
        }

    }
}
