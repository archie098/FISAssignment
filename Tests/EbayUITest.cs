using FISAssignment.Fixture;
using FISAssignment.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class EbayUITest : TestFixture
    {
        [Test]
        public async Task AddBookToEbayCart_UpdatesCartCount()
        {
            Test.Info("Navigating to eBay and searching for Book");
            await searchPage.SearchAndSelectFirstResultAsync(config.applicationURL, "Book");

            Test.Info("Search completed and first result selected");
            var targetPage = Driver.Context.Pages.Last();

            var productPage = new ProductPage(targetPage);
            Test.Info("Adding product to cart");
            await productPage.AddToCartAsync();

            var cartPage = new CartPage(targetPage);
            Test.Info("Retrieving cart quantity");
            var cartCount = await cartPage.GetCartCountAsync();

            Assert.That(cartCount, Is.EqualTo("1"), "Expected 1 item in the cart, but the count did not match.");
        }

    }
}
