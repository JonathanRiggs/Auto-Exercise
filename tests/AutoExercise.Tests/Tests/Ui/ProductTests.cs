using AutoExercise.Tests.Pages;
using Microsoft.Playwright.NUnit;

namespace AutoExercise.Tests.Tests.Ui;

public class ProductTests : PageTest
{
    [Test, Description("TC-09 Search Product")]
    public async Task Search_ShowsOnlyMatchingProducts()
    {
        var home = new HomePage(Page);
        await home.GotoAsync();
        await home.ExpectLoadedAsync();

        await home.Nav.GoToProductsAsync();
        var products = new ProductsPage(Page);
        await products.ExpectLoadedAsync();

        await products.SearchAsync("Jeans");
        var names = await products.GetProductNamesAsync();
        Assert.That(names, Is.Not.Empty);
        Assert.That(names, Has.All.Contains("Jeans").IgnoreCase);
    }

    [Test, Description("TC-08 Verify All Products and Product Detail page")]
    public async Task FirstProduct_DetailPageShowsAllFields()
    {
        var products = new ProductsPage(Page);
        await products.GotoAsync();
        await products.ExpectLoadedAsync();

        var listedName = (await products.GetProductNamesAsync())[0];
        await products.ViewProductAsync(0);

        var detail = new ProductDetailPage(Page);
        await detail.ExpectLoadedAsync();

        foreach (var field in detail.RequiredFields)
            await Expect(field).ToBeVisibleAsync();

        // Fields contain real values and match the listing
        var details = await detail.GetDetailsAsync();
        Assert.Multiple(() =>
        {
            Assert.That(details.Name, Is.EqualTo(listedName));
            Assert.That(details.Price, Does.Match(@"^Rs\.\s*\d+$"));
            Assert.That(details.Category, Is.Not.Empty);
            Assert.That(details.Availability, Is.Not.Empty);
            Assert.That(details.Condition, Is.Not.Empty);
            Assert.That(details.Brand, Is.Not.Empty);
        });
    }
}


