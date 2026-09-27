using FISAssignment.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FISAssignment.ApiHelper
{
    public class APIBase
    {
        protected readonly IAPIRequestContext? _request;
        protected readonly AppSettingsSchema config = ConfigManager.ReadConfig();

        protected APIBase(IAPIRequestContext request)
        {
            _request = request;
        }

    }
}
