using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.UI.Pages
{
    internal class CheckoutPage
    {
        private readonly IWebDriver _driver;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement FirstNameField => _driver.FindElement(By.Id("first-name"));
        private IWebElement LastNameField => _driver.FindElement(By.Id("last-name"));
        private IWebElement PostalCodeField => _driver.FindElement(By.Id("postal-code"));
        private IWebElement ContinueButton => _driver.FindElement(By.Id("continue"));

        private IWebElement FinishButton => _driver.FindElement(By.Id("finish"));
        private IWebElement CompleteHeader => _driver.FindElement(By.ClassName("complete-header"));

        public void FillInformation(string firstName, string lastName, string postalCode)
        {
            FirstNameField.SendKeys(firstName);
            LastNameField.SendKeys(lastName);
            PostalCodeField.SendKeys(postalCode);

            ContinueButton.Click();
        }

        public void ClickFinish()
        {
            FinishButton.Click();
        }

        public string GetConfirmationMessage()
        {
            return CompleteHeader.Text;
        }
    }
}
