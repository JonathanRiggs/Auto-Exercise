using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutoExercise.Tests.Components;

// Top Nav bar present on all pages. Scoped to .shop-menu

public class HeaderNav(IPage page)
{
    private ILocator Menu => page.Locator(".shop-menu");

    private ILocator Link(string name) => Menu.GetByRole(AriaRole.Link, new() { Name = name });

    public ILocator Home => Link("Home");
    public ILocator Products => Link("Products");
    public ILocator Cart => Link("Cart");
    public ILocator SignupLogin => Link("Signup / Login");
    public ILocator TestCases => Link("Test Cases");
    public ILocator ContactUs => Link("Contact Us");

    public ILocator LoggedInAs => Menu.GetByText("Logged in as");
    public ILocator Logout => Link("Logout");
    public ILocator DeleteAccount => Link("Delete Account");

    public Task GoToHomeAsyunc() => Home.ClickAsync();
    public Task GoToProductsAsync() => Products.ClickAsync();
    public Task GoToCartAsync() => Cart.ClickAsync();
    public Task GoToSignupLoginAsync() => SignupLogin.ClickAsync();
    public Task GoToTestCasesAsync() => TestCases.ClickAsync();
    public Task GoToContactUsAsync() => ContactUs.ClickAsync();
    public Task LogoutAsync() => Logout.ClickAsync();
    public Task DeleteAccountAsync() => DeleteAccount.ClickAsync();
}