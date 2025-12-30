
using Microsoft.AspNetCore.ResponseCompression;
using Mtc.Framwork.AppCore;
using Mtc.Host.IService;
using Mtc.Host.Service;
using Mtc.Host.WebApi.TimeTask;
using NLog.Extensions.Logging;

namespace Mtc.Host.WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // 日志
        builder.Logging.AddNLog();
        // 向容器中添加服务
        builder.Services.AddControllers();
        // 了解有关配置Swagger/OpenAPI的更多信息，请访问https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // ThreadPool设置
        builder.Services.AddThreadPoolSetup(2, 6);
        builder.Services.AddHttpContextAccessor();
        // AddMvc
        builder.Services.AddTransient<ICustomerService, CustomerService>();
        builder.Services.AddMvc(Options =>
        {
            // 统一错误处理
            Options.Filters.Add<NotImplExceptionFilterAttribute>();
        });
        // 跨域
        builder.Services.AddCorsSetup();
        // 压缩
        builder.Services.AddResponseCompression(o =>
        {
            o.Providers.Add<BrotliCompressionProvider>();
            o.Providers.Add<GzipCompressionProvider>();
        });
        // 任务
        builder.Services.AddHostedService<JobTaskService>();
        // 构建
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // CORS跨域
        app.UseCors(AppSettingsConst.CorsPolicyName);
        // 中间件---限流
        app.UseMiddleware<WebLimitingMiddleware>();
        // Https
        app.UseHttpsRedirection();
        // 中间件用于授权用户访问资源
        app.UseAuthorization();
        // 控制器建立路由约定
        app.MapControllers();
        // 压缩
        app.UseResponseCompression();

        app.Run();
    }
}
