using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.UI.Pages
{
    internal class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameField => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));

        public void LoginUser(string username, string password)
        {
            UsernameField.Clear();
            PasswordField.Clear();

            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
