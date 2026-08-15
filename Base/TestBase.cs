using AventStack.ExtentReports;
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
        protected ExtentTest _test = null!;


        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            ExtentManager.GetInstance();
        }
        
        [SetUp]
        public void SetUp()
        {
            _test = ExtentManager.GetInstance().CreateTest(TestContext.CurrentContext.Test.Name);
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        public void TakeScreenshot(string name)
        {
            string directoryPath = @"C:\Tests";
            Directory.CreateDirectory(directoryPath);


            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            string filePath = Path.Combine(directoryPath, $"Error{name}.png");

            ss.SaveAsFile(filePath);
            TestContext.Progress.WriteLine($"Screenshot saved in: {filePath}");
        }

        [TearDown]
        public void CleanUp()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            string testName = TestContext.CurrentContext.Test.Name;

            if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
                _test.Fail($"Test failed: {errorMessage}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                TakeScreenshot(testName);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _test.Pass("Test passed");
            }
            else
            {
                _test.Skip("Test was skipped");
            }

            _driver?.Quit();
            _driver?.Dispose();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            ExtentManager.GetInstance().Flush();
        }
    }
}
