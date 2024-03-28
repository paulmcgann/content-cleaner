using ContentCleaner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ContentCleaner.Config
{
    public static class ContentCleanerExtensions
    {
        public static IServiceCollection AddContentCleaner(this IServiceCollection services, Action<AuthorizationOptions>? authorizationOptions = null)
        {
            services.AddSingleton<IContentService, ContentService>();

            if (authorizationOptions != null)
            {
                services.AddAuthorization(authorizationOptions);
            }
            else
            {
                var allowedRoles = new List<string> { "CmsAdmins", "Administrator", "WebAdmins" };
                services.AddAuthorization(authorizationOptions =>
                        {
                            authorizationOptions.AddPolicy(Constants.AuthorizationPolicy, policy =>
                            {
                                policy.RequireRole(allowedRoles);
                            });
                        });
            }

            return services;
        }

        public static IApplicationBuilder AddContentCleaner(this IApplicationBuilder builder)
        {
            return builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ContentCleanerAdmin",
                    pattern: "{controller=ContentCleanerAdmin}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute("ContentCleanerAdminApi", "api/{controller}/{action}/{id?}");
            });
        }
    }
}