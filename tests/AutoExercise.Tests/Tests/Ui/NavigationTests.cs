using AutoExercise.Tests.Pages;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Internal;

namespace AutoExercise.Tests.Tests.Ui;

public class NavigationTests : PageTest
{
    [Test, Description("TC-07 Navigation - Verify Test Cases Page")]
    public async Task TestCasesLink_NavigatesToTestCasesPage()
    {
        var home = new HomePage(Page);
        await home.GotoAsync();
        await home.ExpectLoadedAsync();

        await home.Nav.GoToTestCasesAsync();

        var testCases = new TestCasesPage(Page);
        await testCases.ExpectLoadedAsync();
        await Expect(testCases.TestCaseTitle(1)).ToContainTextAsync("Register User");
    }
}