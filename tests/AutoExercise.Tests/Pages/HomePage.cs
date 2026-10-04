using System.Text.RegularExpressions;
using AutoExercise.Tests.Components;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Landing page at "/" 

public class HomePage(IPage page)
{
    private const string HeroText = "Full-Fledged practice website for Automation Engineers";

    public HeaderNav Nav { get; } = new(page);

    public ILocator Logo => page.GetByAltText("Website for automation practice");

    // Hero carousel repeats the heading once per slide; .First targets the active one
    public ILocator HeroHeading => page.Locator("#slider").GetByText(HeroText).First;

    public async Task GoToAsync() => await page.GotoAsync("/");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"automationexercise\.com/?$"));
        await Expect(Logo).ToBeVisibleAsync();
        await Expect(HeroHeading).ToBeVisibleAsync();
    }
}