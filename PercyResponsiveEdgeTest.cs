using NUnit.Framework;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using PercyIO.Selenium;

namespace PercyOnAutomate
{
    // Responsive DOM snapshot of an Edge session on Automate. Percy.Snapshot()
    // needs a Percy web project token; the Automate project token used by
    // PercyTest only accepts Percy.Screenshot().
    [TestFixture]
    [Category("sample-percy-responsive-edge")]
    public class PercyResponsiveEdgeTest
    {
        private RemoteWebDriver? driver;

        [SetUp]
        public void Init()
        {
            var browserstackOptions = new Dictionary<string, object>
            {
                { "os", "Windows" },
                { "osVersion", "11" },
                { "projectName", "My Project" },
                { "buildName", "test percy_snapshot responsive" },
                { "sessionName", "Percy responsive Edge" },
                { "local", "false" },
                // Automate only hands back a webSocketUrl when BiDi is enabled
                // here too, and BiDi needs a 4.20+ Selenium server jar.
                { "seleniumBidi", true },
                { "seleniumVersion", "4.33.0" },
                { "userName", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME") ?? "" },
                { "accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY") ?? "" },
            };
            var options = new EdgeOptions
            {
                BrowserVersion = "latest",
                // WebDriver BiDi: lets the SDK set exact viewport widths and
                // read closed shadow roots where CDP is unavailable.
                UseWebSocketUrl = true,
            };
            options.AddAdditionalOption("bstack:options", browserstackOptions);
            driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub"), options);
        }

        [Test]
        public void ResponsiveHomePage()
        {
            driver!.Navigate().GoToUrl("https://bstackdemo.com/");
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(d => d.Title.Contains("StackDemo"));

            Percy.Snapshot(driver, "Home page - responsive Edge", new Dictionary<string, object>
            {
                { "widths", new List<int> { 375, 1280 } },
                { "responsiveSnapshotCapture", true },
            });
        }

        [TearDown]
        public void Cleanup()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
