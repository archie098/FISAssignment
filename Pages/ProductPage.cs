using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Pages
{
    public class ProductPage
    {
        private readonly IPage? _page;
        private ILocator AddToCartButton => _page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" });
        private ILocator SeeInCartButton => _page.GetByRole(AriaRole.Link, new() { Name = "See in cart" });
        public ProductPage(IPage page)
        {
            _page = page;
        }

        public async Task AddToCartAsync()
        {
            await AddToCartButton.ClickAsync();
            await SeeInCartButton.ClickAsync();
        }
    }
}
