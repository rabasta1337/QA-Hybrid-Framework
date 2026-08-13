using QA_Hybrid_Framework.Base;
using QA_Hybrid_Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.Tests
{
    public class UiTests : TestBase
    {
        [Test]
        public void SuccessfulLoginAndAddToCartTest()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(_driver);
            loginPage.LoginUser("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(_driver);
            inventoryPage.AddBackpackToCart();

            Assert.That(inventoryPage.GetCartBadgeText(), Is.EqualTo("1"));
        }

        [Test]
        public void IntentionalFailureForScreenshotTest()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(_driver);
            loginPage.LoginUser("standard_user", "secret_sauce");

           
            Assert.That(_driver.Url, Does.Contain("page-that-does-not-really-exist-cheers")); //for screenshot
        }
    }
}
