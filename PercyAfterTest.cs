using NUnit.Framework;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using PercyIO.Selenium;

namespace PercyOnAutomate
{
    [TestFixture]
    [Category("sample-percy-after-test")]
    public class PercyAfterTest
    {

    string USERNAME = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
    string ACCESS_KEY = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
    string BROWSERSTACK_URL = "https://hub-cloud.browserstack.com/wd/hub";
    protected RemoteWebDriver driver;

    protected WebDriverWait wait;

    public PercyAfterTest() : base() { }

    [SetUp]
    public void Init()
    {

        System.Collections.Generic.Dictionary<string, object> browserstackOptions =
        new Dictionary<string, object>();
        browserstackOptions.Add("browserName","Firefox");
        browserstackOptions.Add("browserVersion", "latest");
        browserstackOptions.Add("os","Windows");
        browserstackOptions.Add("osVersion","10");
        browserstackOptions.Add("projectName","Percy");
        browserstackOptions.Add("buildName","Selenium-SDKs");
        browserstackOptions.Add("sessionName","win11-dotnet-sel");
        browserstackOptions.Add("local","false");
        browserstackOptions.Add("seleniumVersion","3.14.0");
        browserstackOptions.Add("userName",USERNAME);
        browserstackOptions.Add("accessKey",ACCESS_KEY);
        ChromeOptions options = new ChromeOptions();
        options.AddAdditionalOption("bstack:options", browserstackOptions);
        driver = new RemoteWebDriver(
        new Uri(BROWSERSTACK_URL),
        options
      );

    }

        [Test]
        public void SearchBstackDemo()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
            driver.Navigate().GoToUrl("https://bstackdemo.com/");
            wait.Until(d => d.Title.Contains("StackDemo"));

            // click on the samsung products
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='__next']/div/div/main/div[1]/div[2]/label/span"))).Click();
            
            // [percy note: important step]
            // Percy Screenshot 1
            // take percy_screenshot using the following command
            Percy.Screenshot(driver, "screenshot_1");

            // Get text of current product
            string productOnPageText = driver.FindElement(By.XPath("//*[@id=\"10\"]/p")).Text;

            // clicking on 'Add to cart' button
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"10\"]/div[4]"))).Click();
           

            //Check if the Cart pane is visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@class=\"float-cart__content\"]")));
            
            string productOnCartText = driver.FindElement(By.XPath("//*[@id='__next']/div/div/div[2]/div[2]/div[2]/div/div[3]/p[1]")).Text;

            // [percy note: important step]
            // Percy Screenshot 2
            // take percy_screenshot using the following command
            Percy.Screenshot(driver, "screenshot_2");
            
            Assert.AreEqual(productOnCartText, productOnPageText);

        }

        

        [TearDown]
        public void Cleanup()
        {
          driver.Quit();
        }
    }

    
}
