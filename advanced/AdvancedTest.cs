// PER-8195 Phase 3 — automate-selenium-dotnet advanced example.

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PercyIO.Selenium;

namespace PercyAutomateAdvanced;

[TestFixture]
public class AdvancedTests
{
    private RemoteWebDriver? _driver;

    [OneTimeSetUp]
    public void SetUp()
    {
        var bstackOptions = new Dictionary<string, object>
        {
            { "browserName", "Chrome" },
            { "browserVersion", "latest" },
            { "os", "Windows" },
            { "os_version", "11" },
            { "projectName", Environment.GetEnvironmentVariable("PERCY_PROJECT") ?? "Percy Automate Selenium-.NET Advanced" },
            { "buildName", Environment.GetEnvironmentVariable("PERCY_BUILD") ?? "Advanced Selenium .NET" },
            { "sessionName", "advanced_visual_test" },
            { "userName", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME") ?? "" },
            { "accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY") ?? "" },
        };
        var options = new ChromeOptions();
        options.AddAdditionalOption("bstack:options", bstackOptions);
        _driver = new RemoteWebDriver(
            new Uri("https://hub-cloud.browserstack.com/wd/hub"), options);
        _driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
        _driver.Navigate().GoToUrl("https://bstackdemo.com/");
    }

    [OneTimeTearDown]
    public void TearDown() => _driver?.Quit();

    [Test]
    public void ExercisesBaseline()
    {
        Percy.Screenshot(_driver!, "BStackDemo — baseline");
    }

    [Test]
    public void ExercisesIgnoreRegionXpaths()
    {
        Percy.Screenshot(_driver!, "BStackDemo — ignore via xpath", new Dictionary<string, object>
        {
            { "ignore_region_xpaths", new[] { "//*[@id=\"signin\"]" } },
        });
    }

    [Test]
    public void ExercisesIgnoreRegionSelectors()
    {
        Percy.Screenshot(_driver!, "BStackDemo — ignore via CSS selector", new Dictionary<string, object>
        {
            { "ignore_region_selectors", new[] { "#signin", ".shelf-container-header" } },
        });
    }

    [Test]
    public void ExercisesCustomIgnoreRegions()
    {
        var region = new Dictionary<string, object>
        {
            { "top", 0 }, { "bottom", 100 }, { "left", 0 }, { "right", 1280 },
        };
        Percy.Screenshot(_driver!, "BStackDemo — custom ignore region", new Dictionary<string, object>
        {
            { "custom_ignore_regions", new[] { region } },
        });
    }

    [Test]
    public void ExercisesConsiderRegionXpaths()
    {
        Percy.Screenshot(_driver!, "BStackDemo — consider via xpath", new Dictionary<string, object>
        {
            { "consider_region_xpaths", new[] { "//*[@id=\"__next\"]" } },
        });
    }

    [Test]
    public void ExercisesFreezeAnimation()
    {
        Percy.Screenshot(_driver!, "BStackDemo — freeze_animation", new Dictionary<string, object>
        {
            { "freeze_animation", true },
        });
    }

    [Test]
    public void ExercisesPercyCss()
    {
        Percy.Screenshot(_driver!, "BStackDemo — percy_css", new Dictionary<string, object>
        {
            { "percy_css", ".shelf-container { background: #fffde7 !important; }" },
        });
    }

    [Test]
    public void ExercisesSyncMode()
    {
        Percy.Screenshot(_driver!, "BStackDemo — sync", new Dictionary<string, object>
        {
            { "sync", true },
        });
    }

    [Test]
    public void ExercisesTestCaseAndLabels()
    {
        Percy.Screenshot(_driver!, "BStackDemo — test_case + labels", new Dictionary<string, object>
        {
            { "test_case", "home-smoke" },
            { "labels", "smoke,automate-selenium-dotnet" },
        });
    }
}
