using FISAssignment.APIClient;
using FISAssignment.Driver;
using FISAssignment.Pages;
using FISAssignment.Utils;
using Microsoft.Playwright;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Fixture
{
    public class TestFixture
    {
        private PlaywrightDriver _driver = null!;
        private SearchPage? _searchPage;
        private BitCoinClient? _apiClient;
        private string _screenshotFolder = null!;
        private AppSettingsSchema _config = null!;
        protected PlaywrightDriver Driver => _driver;
        private static ExtentReports? _extent;
        protected ExtentTest Test = null!;
        protected virtual bool CaptureScreenshot => true;
        protected AppSettingsSchema config
        {
            get { return _config; }
            private set { _config = value; }
        }
        public SearchPage searchPage
        {
            get { return _searchPage; }
            private set { _searchPage = value; }
        }
        public BitCoinClient apiClient
        {
            get { return _apiClient; }
            private set { _apiClient = value; }
        }

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            _extent = ExtentReportManager.GetInstance();
        }

        [SetUp]
        public async Task BeforeEachTest()
        {
            config = ConfigManager.ReadConfig();
            _driver = new PlaywrightDriver();
            await _driver.InitializeAsync();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug\net8.0", "");

            _screenshotFolder = Path.Combine(projectRoot,"TestResults","Screenshots");

            Directory.CreateDirectory(_screenshotFolder);

            searchPage = new SearchPage(_driver.Page);

            var playwright = await Playwright.CreateAsync();

            var apiRequestContext = await playwright.APIRequest.NewContextAsync();

            apiClient = new BitCoinClient(apiRequestContext);
            Test = _extent!.CreateTest(TestContext.CurrentContext.Test.Name);
            Test.Info("Test Started");
        }

        [TearDown]
        public async Task AfterEachTest()
        {
            string fullPath = string.Empty;
            string statusLabel;
            try
            {
                if (CaptureScreenshot && _driver != null && _driver.Context != null && _driver.Context.Pages.Any())
                {
                    var activePage = _driver.Context.Pages.LastOrDefault() ?? _driver.Page;

                    if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                    {
                        statusLabel = "FAILED";
                    }
                    else
                    {
                        statusLabel = "PASSED";
                    }
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string fileName = $"{TestContext.CurrentContext.Test.Name}_{statusLabel}_{timestamp}.png";
                    fullPath = Path.Combine(_screenshotFolder, fileName);

                    await activePage.ScreenshotAsync(new Microsoft.Playwright.PageScreenshotOptions { Path = fullPath });
                    TestContext.AddTestAttachment(fullPath, $"Final Screen ({statusLabel})");

                        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                        {
                            Test.Pass("Test Passed");
                        }
                        else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                        {
                            Test.Fail(TestContext.CurrentContext.Result.Message);
                        }
                        else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
                        {
                            Test.Skip("Test Skipped");
                        }

                        if (!string.IsNullOrEmpty(fullPath))
                        {
                            Test.AddScreenCaptureFromPath(fullPath);
                        }
                }
            }
            finally
            {
                if (_driver != null)
                {
                    await _driver.DisposeAsync();
                }

            }
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            ExtentReportManager.FlushReport();
        }
    }
}
