using GatewayAPI.Extentions.Handlers;

namespace GatewayAPI.Extentions.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}