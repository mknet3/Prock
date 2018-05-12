using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace Prock.Core
{
    public static class MockApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMocks(this IApplicationBuilder app, params IMock[] mocks)
        {
            return app.UseMocks(configuration: null, mocks: mocks);
        }

        public static IApplicationBuilder UseMock(this IApplicationBuilder app, IMock mock)
        {
            return app.UseMock(configuration: null, mock: mock);
        }

        public static IApplicationBuilder UseMocks(this IApplicationBuilder app, Action<IApplicationBuilder> configuration, params IMock[] mocks)
        {
            foreach (var mock in mocks)
            {
                app.UseMock(configuration, mock);
            }

            return app;
        }

        public static IApplicationBuilder UseMock(this IApplicationBuilder app, Action<IApplicationBuilder> configuration, IMock mock)
        {
            app.Map(mock.Route, appBranch =>
            {
                configuration?.Invoke(appBranch);
                appBranch.Run(async context =>
                {
                    context.Response.ContentType = mock.ContentType;
                    context.Response.StatusCode = mock.StatusCode;
                    await context.Response.WriteAsync(mock.Json);
                });
            });

            return app;
        }
    }
}
