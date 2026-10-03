using AutoExercise.Tests.Fixtures;
using AutoExercise.Tests.Pages;

namespace AutoExercise.Tests.Tests.Ui;

[Category("UI")]
public class LoginTests : BaseUiTest
{
    [Test, Description("TC-03 Login User with incorrect email and password")]
    public async Task Login_WithInvalidCredentials_ShowsError()
    {
        var login = new LoginPage(Page);
        await login.GotoAsync();
        await login.LoginAsync("nobody@example.com", "wrong-password");

        await Expect(login.Error).ToBeVisibleAsync();
    }
}