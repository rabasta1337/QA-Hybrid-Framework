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
        // Pola chronione dostępne dla klas dziedziczących
        protected IWebDriver _driver;
        protected ExtentTest _test = null!;

        // Uruchamiane jednorazowo przed wszystkimi testami w danej klasie testowej
        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            ExtentManager.GetInstance();
        }
        // Uruchamiane przed kazdym testem zeby miec czyste srodowisko testowe
        [SetUp]
        public void SetUp()
        {
            // Rejestracja testu w raporcie ExtentReports na podstawie aktualnej nazwy metody testowej
            _test = ExtentManager.GetInstance().CreateTest(TestContext.CurrentContext.Test.Name);
           
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        // Metoda pomocnicza zapisująca zrzut ekranu jako fizyczny plik .png
        public void TakeScreenshot(string name)
        {
            string directoryPath = @"C:\Tests";
            Directory.CreateDirectory(directoryPath); //jak folder juz istnieje to jest skipowane


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
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString; //rzutowanie drivera na interfejs ITakesScreenshot zeby miec dostep do metody GetScreenshot() ktora zapisze nam fote bezposrednio w raporcie html
                _test.Fail($"Test failed: {errorMessage}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                // Zapis fizycznego pliku 
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

            //zamknięcie przeglądarki i zwolnienie zasobów dla czystego srodowiska
            _driver?.Quit();
            _driver?.Dispose();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            ExtentManager.GetInstance().Flush(); // po wsyzstkich testach flushujemy extentmanagera tworzac tym samym lub nadpisuajc TestReport.htmkl na dysku
        }
    }
}
