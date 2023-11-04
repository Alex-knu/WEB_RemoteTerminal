using GatewayAPI.Extentions.Handlers;
using Microsoft.AspNetCore.Builder;

namespace GatewayAPI.Extentions.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}