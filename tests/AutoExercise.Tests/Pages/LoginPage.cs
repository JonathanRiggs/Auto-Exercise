using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AutoExercise.Tests.Components;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Update holds two forms. Existing user login and new user signup

public class LoginPage(IPage page)
{
    public HeaderNav Nav { get; } = new(page);

    // Login Form
    public ILocator Loginheading => 
        page.GetByRole(AriaRole.Heading, new() { Name = "Login to account" });
    public ILocator Email => page.GetByTestId("login-email");
    public ILocator Password => page.GetByTestId("login-password");
    public ILocator SubmitButton => page.GetByTestId("login-button");
    public ILocator Error => page.GetByText("Your email or password is incorrect!");

    // Signup form
    public ILocator SignupHeading => 
        page.GetByRole(AriaRole.Heading, new() { Name = "New User Signup" });
    public ILocator SignupName => page.GetByTestId("signup-name");
    public ILocator SignupEmail => page.GetByTestId("signup-email");
    public ILocator SignupButton => page.GetByTestId("signup-button");
    public ILocator SignupError => page.GetByText("Email Address already exist");

    public async Task GotoAsync() => await page.GotoAsync("/login");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/login/?$"));
        await Expect(Loginheading).ToBeVisibleAsync();
        await Expect(SignupHeading).ToBeVisibleAsync();
    }

    public async Task LoginAsync(string email, string password)
    {
        await Email.FillAsync(email);
        await Password.FillAsync(password);
        await SubmitButton.ClickAsync();
    }

    public async Task StartSignupAsync(string name, string email)
    {
        await SignupName.FillAsync(name);
        await SignupEmail.FillAsync(email);
        await SignupButton.ClickAsync();
    }
}