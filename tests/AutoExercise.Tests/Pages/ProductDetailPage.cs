using System.Text.RegularExpressions;
using AutoExercise.Tests.Components;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Product detail page. TC-8
public class ProductDetailPage(IPage page)
{
    public HeaderNav Nav { get; } = new(page);

    private ILocator Info => page.Locator(".product-information");

    private ILocator Field(string label) => Info.Locator("p", new() { HasText = $"{label}:" });

    public ILocator Name => Info.Locator("h2");
    public ILocator Category => Field("Category");
    public ILocator Price => Info.GetByText(new Regex(@"^Rs\.\s*\d+"));
    public ILocator Availability => Field("Availability");
    public ILocator Condition => Field("Condition");
    public ILocator Brand => Field("Brand");

    public IReadOnlyList<ILocator> RequiredFields => [ Name, Category, Price, Availability, Condition, Brand ];

    public async Task GoToAsync(int productId) => await page.GotoAsync($"/product_details/{productId}");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/product_details/\d+"));
        await Expect(Info).ToBeVisibleAsync();
    }

    public async Task<ProductDetails> GetDetailsAsync() => new(
        Name: (await Name.InnerTextAsync()).Trim(),
        Category: ValueAfterLabel(await Category.InnerTextAsync()),
        Price: (await Price.InnerTextAsync()).Trim(),
        Availability: ValueAfterLabel(await Availability.InnerTextAsync()),
        Condition: ValueAfterLabel(await Condition.InnerTextAsync()),
        Brand: ValueAfterLabel(await Brand.InnerTextAsync())
    );

    private static string ValueAfterLabel(string text)
    {
        var colon = text.IndexOf(':');
        return (colon >= 0 ? text[(colon + 1)..] : text).Trim();
    }
}

public record ProductDetails(
    string Name,
    string Category,
    string Price,
    string Availability,
    string Condition,
    string Brand
);