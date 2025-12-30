
using Microsoft.Extensions.DependencyInjection;

namespace Mtc.Framwork.AppCore;
public static class CorsSetup
{
    public static void AddCorsSetup(this IServiceCollection services)
    {
        services.AddCors(c =>
        {
            if (!AppSettingsConst.CorsEnableAllIP)
            {
                c.AddPolicy(AppSettingsConst.CorsPolicyName, policy =>
                {
                    policy.WithOrigins(AppSettingsConst.CorsIP.Split(','));
                    policy.AllowAnyHeader();//Ensures that the policy allows any header.
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            }
            else
            {
                //允许任意跨域请求
                c.AddPolicy(AppSettingsConst.CorsPolicyName, policy =>
                {
                    policy.SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            }
        });
    }
}
