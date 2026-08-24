using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ERPBlazorApp;

public class QueryStringCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var culture = httpContext.Request.Query["culture"].ToString();
        if (!string.IsNullOrWhiteSpace(culture))
        {
            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture, culture));
        }
        return Task.FromResult<ProviderCultureResult?>(null);
    }
}