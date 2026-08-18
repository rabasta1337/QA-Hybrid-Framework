using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;


namespace QA_Hybrid_Framework.UI
{
    public static class WaitHelper
    {
        public static IWebElement WaitForElementVisible(IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            // Odpytuje DOM co 500ms  zwraca element gdy Displayed == true, w przeciwnym razie zwraca null i ponawia próbę
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            })!;
        } 

        public static IWebElement WaitForElementClickable(IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            // Zwraca element tylko gdy jest w pełni interaktywny dla użytkownika
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            })!;
        }


    }
}
