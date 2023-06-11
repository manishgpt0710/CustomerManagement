using System;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CustomerManagement.IntegrationTests.MiddlewareHelpers
{
    public class FakeApiKeyAuthMiddleware
    {
        private readonly RequestDelegate next;

        public FakeApiKeyAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.Headers["x-api-key"] = "cmVhZC1vbmx5LWtleQ==";

            await next(httpContext);
        }
    }
}
