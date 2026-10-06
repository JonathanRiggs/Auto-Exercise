using System.Text.RegularExpressions;
using AutoExercise.Tests.Data;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Step 2 of registration. Name Email are prefilled from LoginPage
public class SignupPage(IPage page)
{
    public ILocator AccountInfoHeading => page.GetByRole(AriaRole.Heading, new() { Name = "Enter Account information" });

    public ILocator TitleMr => page.Locator("#id_gender1");
    public ILocator TitleMrs => page.Locator("#id_gender2");
    public ILocator Name => page.GetByTestId("name");
    public ILocator Email => page.GetByTestId("email");
    public ILocator Password => page.GetByTestId("password");
    public ILocator BirthDay => page.GetByTestId("days");
    public ILocator BirthMonth => page.GetByTestId("months");
    public ILocator BirthYear => page.GetByTestId("years");
    public ILocator Newsletter => page.Locator("#newsletter");
    public ILocator SpecialOffers => page.Locator("#optin");

    public ILocator FirstName => page.GetByTestId("first_name");
    public ILocator LastName => page.GetByTestId("last_name");
    public ILocator Company => page.GetByTestId("company");
    public ILocator Address1 => page.GetByTestId("address");
    public ILocator Address2 => page.GetByTestId("address2");
    public ILocator Country => page.GetByTestId("country");
    public ILocator State => page.GetByTestId("state");
    public ILocator City => page.GetByTestId("city");
    public ILocator Zipcode => page.GetByTestId("zipcode");
    public ILocator MobileNumber => page.GetByTestId("mobile_number");

    public ILocator CreateAccountButton => page.GetByTestId("create-account");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/signup/?$"));
        await Expect(AccountInfoHeading).ToBeVisibleAsync();
    }

    public async Task FillAccountInformationAsync(UserAccount user, bool newsletter = true, bool specialOffers = true)
    {
        await (user.Title == "Mrs" ? TitleMrs : TitleMr).CheckAsync();
        await Password.FillAsync(user.Password);

        await BirthDay.SelectOptionAsync(user.BirthDay.ToString());
        await BirthMonth.SelectOptionAsync(user.BirthMonth.ToString());
        await BirthYear.SelectOptionAsync(user.BirthYear.ToString());

        await Newsletter.SetCheckedAsync(newsletter);
        await SpecialOffers.SetCheckedAsync(specialOffers);

        await FirstName.FillAsync(user.FirstName);
        await LastName.FillAsync(user.LastName);
        await Company.FillAsync(user.Company);
        await Address1.FillAsync(user.Address1);
        await Address2.FillAsync(user.Address2);
        await Country.FillAsync(user.Country);
        await State.FillAsync(user.State);
        await City.FillAsync(user.City);
        await Zipcode.FillAsync(user.Zipcode);
        await MobileNumber.FillAsync(user.MobileNumber);
    }

    public async Task CreateAccountAsync()
    {
        await CreateAccountButton.ClickAsync();
        await page.WaitForURLAsync(new Regex(@"/account_created"));
    }
}