using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace AutoExercise.Tests.Fixtures;

[Parallelizable(ParallelScope.Self)]
public abstract class BaseUiTest : PageTest
{
    [SetUp]
    public void ConfigureSelectors() => Playwright.Selectors.SetTestIdAttribute("data-qa");

    public override BrowserNewContextOptions ContextOptions() => new()
    {
        BaseURL = "https://automationexercise.com",
        ViewportSize = new ViewportSize { Width = 1280, Height = 800 },
    };

    [SetUp]
    public async Task BlockAds() => 
        await Context.RouteAsync(
            new Regex(@"(googlesyndication|doubleclick|googleadservices|adservice\.google)"), 
            route => route.AbortAsync());
}