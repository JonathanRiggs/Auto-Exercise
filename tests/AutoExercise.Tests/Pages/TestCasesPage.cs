using System.Text.RegularExpressions;
using AutoExercise.Tests.Components;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Test cases page at "/test_cases". TC-9

public class TestCasesPage(IPage page)
{
    public HeaderNav Nav { get; } = new(page);

    public ILocator Heading => page.GetByRole(AriaRole.Heading, new() { Name = "Test Cases", Exact = true });

    public ILocator TestCaseTitles => page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(@"^Test Case \d+:") });
    public ILocator TestCaseTitle(int number) => page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex($@"^Test Case {number}:") });
    public async Task GotoAsync() => await page.GotoAsync("/test_cases");
    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/test_cases/?$"));
        await Expect(Heading).ToBeVisibleAsync();
        await Expect(TestCaseTitles.First).ToBeVisibleAsync();
    }

    public async Task<IReadOnlyList<string>> GetTestCaseTitlesAsync()
    {
        var titles = await TestCaseTitles.AllInnerTextsAsync();
        return titles.Select(t => t.Trim()).ToList();
    }
}