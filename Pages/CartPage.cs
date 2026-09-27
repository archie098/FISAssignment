using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Pages
{
    public class CartPage
    {
        private readonly IPage _page;
        private ILocator CartQuantity =>_page.Locator("input[type='number']");

        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task<string> GetCartCountAsync()
        {
            await CartQuantity.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            return await CartQuantity.InputValueAsync();
        }
    }
}
