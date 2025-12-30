
using Microsoft.AspNetCore.Http;
using Polly;
using Polly.Bulkhead;

namespace Mtc.Framwork.AppCore;
public class WebLimitingMiddleware
{
    private readonly RequestDelegate next;
    private static BulkheadPolicy bulkhead = Policy.Bulkhead(100, 100000);
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    public WebLimitingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    /// <summary>
    /// 实现
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        await bulkhead.Execute(async () =>
        {
            await next(context);
        });
    }
}
