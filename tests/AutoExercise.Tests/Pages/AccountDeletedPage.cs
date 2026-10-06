using System.Text.RegularExpressions;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

public class AccountDeletedPage(IPage page)
{
    public ILocator Heading => page.GetByTestId("account-deleted");
    public ILocator ContinueButton => page.GetByTestId("continue-button");
 
    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/delete_account"));
        await Expect(Heading).ToBeVisibleAsync();
    }
 
    public Task ContinueAsync() => ContinueButton.ClickAsync();
}