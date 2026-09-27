using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Utils
{
    public class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static ExtentSparkReporter? _sparkReporter;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                string reportPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\Reports\ExtentReport.html");

                reportPath = Path.GetFullPath(reportPath);

                _sparkReporter = new ExtentSparkReporter(reportPath);

                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);

                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Framework", "Playwright");
                _extent.AddSystemInfo("Tester", "Archisman");
            }

            return _extent;
        }

        public static void FlushReport()
        {
            _extent?.Flush();
        }
    }
}
