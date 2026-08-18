using OpenQA.Selenium;
using QA_Hybrid_Framework.Base;
using QA_Hybrid_Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.Tests
{
    [TestFixture]
    [Category("UI")]
    public class UiTests : TestBase
    {

        [Test]
        [Category("Smoke")]
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
        [Category("Regression")]
        public void EndToEndPurchaseFlowTest()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            new LoginPage(_driver).LoginUser("standard_user", "secret_sauce");
            var inventoryPage = new InventoryPage(_driver);
            inventoryPage.AddBackpackToCart();
            inventoryPage.GoToCart();

            new CartPage(_driver).ClickCheckout();

            var checkoutPage = new CheckoutPage(_driver);
            checkoutPage.FillInformation("Jan", "Kowalski", "00-001");
            checkoutPage.ClickFinish();

            Assert.That(checkoutPage.GetConfirmationMessage(), Is.EqualTo("Thank you for your order!"));
        }


        [Test]
        [Category("Regression")]
        public void ProductsSortingByPriceLowToHighTest()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            new LoginPage(_driver).LoginUser("standard_user", "secret_sauce");

            var inventoryPage = new InventoryPage(_driver);
            inventoryPage.SortProducts("lohi");

            var prices = inventoryPage.GetItemPrices();
            Assert.That(prices, Is.Ordered.Ascending, "Prices are not sorted in ascending order!");
        }

        // DATA-DRIVEN TESTING
        [TestCase("locked_out_user", "secret_sauce", "Epic sadface: Sorry, this user has been locked out.")]
        [TestCase("", "secret_sauce", "Epic sadface: Username is required")]
        [TestCase("standard_user", "", "Epic sadface: Password is required")]
        [TestCase("wrong_user", "wrong_pass", "Epic sadface: Username and password do not match any user in this service")]
        [Category("Negative")]
        public void InvalidLogin_ShouldDisplayCorrectErrorMessage(string username, string password, string expectedError)
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(_driver);
            loginPage.LoginUser(username, password);

            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(expectedError));
        }


        [Test]
        public void IntentionalFailureForScreenshotTest()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(_driver);
            loginPage.LoginUser("standard_user", "secret_sauce");


            Assert.That(_driver.Url, Does.Contain("page-that-does-not-really-exist-cheers")); //wywalenie sie asercji oznacza test jako TestStatus.Failed co odpala nam if w testbase wywolujacego screena 
        }

        //session cookie injection
        [Test]
        [Category("Hybrid")]
        public void DirectNavigation_ViaCookieInjection_ShouldBypassLogin()
        {

            _driver.Navigate().GoToUrl("https://www.saucedemo.com/404");
            _driver.Manage().Cookies.AddCookie(new Cookie("session-username", "standard_user"));

            _driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");

            Assert.That(_driver.Url, Does.Contain("inventory.html"));
            Assert.That(_driver.FindElement(By.ClassName("title")).Text, Is.EqualTo("Products"));
        }


    }
}
