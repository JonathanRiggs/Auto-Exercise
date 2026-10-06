using System.Text.RegularExpressions;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

public class AccountCreatedPage(IPage page)
{
    public ILocator Heading => page.GetByTestId("account-created");
    public ILocator ContinueButton => page.GetByTestId("continue-button");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/account_created"));
        await Expect(Heading).ToBeVisibleAsync();
    }

    public Task ContinueAsync() => ContinueButton.ClickAsync();
}