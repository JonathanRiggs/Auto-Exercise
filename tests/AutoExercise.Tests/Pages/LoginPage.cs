using Microsoft.Playwright;

namespace AutoExercise.Tests.Pages;

public class LoginPage(IPage page)
{
    public ILocator Email => page.GetByTestId("login-email");
    public ILocator Password => page.GetByTestId("login-password");
    public ILocator SubmitButton => page.GetByTestId("login-button");
    public ILocator Error => page.GetByText("Your email or password is incorrect!");

    public Task GotoAsync() => page.GotoAsync("/login");

    public async Task LoginAsync(string email, string password)
    {
        await Email.FillAsync(email);
        await Password.FillAsync(password);
        await SubmitButton.ClickAsync();
    }
}