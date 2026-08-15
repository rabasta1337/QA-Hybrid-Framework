using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.Base
{
    internal class ExtentManager
    {
        private static ExtentReports? _extent;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");
                var htmlReporter = new ExtentSparkReporter(reportPath);

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Framework", "Selenium + RestSharp + NUnit");
                _extent.AddSystemInfo("Author", "Radosław");

            }

            return _extent;
        }


    }
}
