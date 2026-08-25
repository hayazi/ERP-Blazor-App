using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace ERPBlazorApp.Hangfire.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
