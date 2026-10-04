using System.Text.RegularExpressions;
using AutoExercise.Tests.Components;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AutoExercise.Tests.Pages;

// Products page at "/products". Covers full catalogue and search results

public class ProductsPage(IPage page)
{
    public HeaderNav Nav { get; } = new(page);

    public ILocator AllProductsHeader => page.GetByRole(AriaRole.Heading, new() { Name = "All Products" });
    public ILocator SearchedProductsHeading => page.GetByRole(AriaRole.Heading, new() { Name = "Searched Products" });
    public ILocator SearchInput => page.Locator("#search_product");
    public ILocator SearchButton => page.Locator("#submit_search");

    public ILocator ProductCards => page.Locator(".features_items .product-image-wrapper");

    // Card has a hover overlay repeating the name and price
    // scope to .productinfo returns each name once
    public ILocator ProductNames => ProductCards.Locator(".productinfo p");

    public ILocator ViewProductLink(int index) => ProductCards.Nth(index).GetByRole(AriaRole.Link, new() { Name = "View Product" });

    public async Task GogoAsync() => await page.GotoAsync("/products");

    public async Task ExpectLoadedAsync()
    {
        await Expect(page).ToHaveURLAsync(new Regex(@"/products/?$"));
        await Expect(AllProductsHeader).ToBeVisibleAsync();
        await Expect(ProductCards.First).ToBeVisibleAsync();
    }

    // Submit a search and wait for results page to load.
    public async Task SearchAsync(string term)
    {
        await SearchInput.FillAsync(term);
        await SearchButton.ClickAsync();
        await page.WaitForURLAsync(new Regex(@"[?&]search="));
    }

    // Open detail page of the product at given position
    public async Task ViewProductAsync(int index=0)
    {
        await ViewProductLink(index).ClickAsync();
        await page.WaitForURLAsync(new Regex(@"/product_details/\d+"));
    }

    public async Task<IReadOnlyList<string>> GetProductNamesAsync()
    {
        var names = await ProductNames.AllInnerTextsAsync();
        return names.Select(n => n.Trim()).ToList();
    }
}