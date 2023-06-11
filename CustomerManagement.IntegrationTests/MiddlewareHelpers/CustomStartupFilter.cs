using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CustomerManagement.IntegrationTests.MiddlewareHelpers
{
    public class CustomStartupFilter : IStartupFilter
    {
        public CustomStartupFilter()
        {
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<FakeApiKeyAuthMiddleware>();
                next(app);
            };
        }
    }
}

