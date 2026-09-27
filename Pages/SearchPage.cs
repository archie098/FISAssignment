using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Pages
{
    public class SearchPage
    {
        private readonly IPage? _page;
        private ILocator SearchInput => _page.GetByRole(AriaRole.Combobox,new() { Name = "Search for anything" });
        private ILocator FirstResultLink => _page.Locator("(//ul[contains(@class,'srp-results')]//a[contains(@class,'s-card__link')])[1]");
        public SearchPage(IPage page)
        {
            _page = page;
        }

        public async Task SearchAndSelectFirstResultAsync(string baseUrl, string searchTerm)
        {
            await _page.GotoAsync(baseUrl);

            await SearchInput.FillAsync(searchTerm);
            await SearchInput.PressAsync("Enter");

            var popupTask = _page.WaitForPopupAsync();
            await FirstResultLink.ClickAsync();

            var newTab = await popupTask;
            await newTab.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }


    }
}
