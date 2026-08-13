using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.Base
{
    public class TestBase
    {
        protected IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        public void TakeScreenshot(string name)
        {
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            string filePath = $@"C:\Tests\Error_{name}.png";

            ss.SaveAsFile(filePath);
            TestContext.Progress.WriteLine($"Screenshot saved in: {filePath}");
        }

        [TearDown]
        public void CleanUp()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string testName = TestContext.CurrentContext.Test.Name;
                TakeScreenshot(testName);
            }

           
            _driver?.Dispose();
        }
    }
}
