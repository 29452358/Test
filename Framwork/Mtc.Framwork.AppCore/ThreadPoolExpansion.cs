using Microsoft.Extensions.DependencyInjection;

namespace Mtc.Framwork.AppCore;
public static class ThreadPoolExpansion
{
    /// <summary>
    /// 服务注册
    /// </summary>
    /// <param name="app"></param>
    /// <param name="lifetime"></param>
    /// <param name="Configuration"></param>
    public static void AddThreadPoolSetup(this IServiceCollection services, int min, int max)
    {
        ThreadPool.SetMinThreads(min, min);
        ThreadPool.SetMaxThreads(max, max);
    }
}
