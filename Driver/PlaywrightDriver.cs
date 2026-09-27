using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISAssignment.Driver
{
    public class PlaywrightDriver
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private IPage? _page;

        public IPage Page => _page!;
        public IBrowserContext Context => _context!;

        public async Task InitializeAsync(bool headless = false)
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                SlowMo = headless ? 0 : 300
            });

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        public async Task DisposeAsync()
        {
            if (_page != null) await _page.CloseAsync();
            if (_context != null) await _context.DisposeAsync();
            if (_browser != null) await _browser.DisposeAsync();
            _playwright?.Dispose();
        }

    }
}
