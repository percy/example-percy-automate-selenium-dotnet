using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PercyIO.Selenium;

namespace PercyWeb
{
    [TestFixture]
    [Category("sample-web-percy-test")]
    public class PercyWebTest
    {
			 private FirefoxDriver driver; // Declare at class level (don't initialize here)

        [SetUp]
        public void Setup()
        {
            var options = new FirefoxOptions(); // Create options inside the method
            driver = new FirefoxDriver(options); // Now you can initialize the driver
        }

        [Test]
        public void BasicTest()
        {
            driver.Navigate().GoToUrl("http://example.com");
						Percy.Snapshot(driver, ".NET example");
						// Percy.Snapshot(driver, ".NET anonymous options", new {  widths = new [] { 600, 1200 }});
						Percy.Options snapshotOptions = new Percy.Options();
						snapshotOptions.Add("fullPage", true);
						Percy.Snapshot(driver, "FullPage", snapshotOptions);
						}

        [TearDown]
        public void Cleanup()
        {
          driver.Quit();
        }
    }
    
}
