using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA_Hybrid_Framework.Base
{
    internal class ExtentManager
    {
        // Prywatne statyczne pole przechowujące jedyną instancję silnika raportującego
        private static ExtentReports? _extent;

        public static ExtentReports GetInstance()
        {

            //Tworzymy obiekt tylko przy pierwszym wywołaniu

            if (_extent == null)
            {
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");
                // Silnik renderujący widok HTML w motywie Spark
                var htmlReporter = new ExtentSparkReporter(reportPath);

                // Inicjalizacja głównego menedżera raportu i podpięcie generatora HTML
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);

                // Konfiguracja metadanych środowiskowych wyświetlanych na dashboardzie raportu
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Framework", "Selenium + RestSharp + NUnit");
                _extent.AddSystemInfo("Author", "Radosław");

            }

            return _extent;
        }


    }
}
