
using Mtc.Host.IService;
using Mtc.Host.Service;

namespace Mtc.Host.WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // 向容器中添加服务
        builder.Services.AddControllers();
        // 了解有关配置Swagger/OpenAPI的更多信息，请访问https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        // 构建
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Https
        app.UseHttpsRedirection();
        // 中间件用于授权用户访问资源
        app.UseAuthorization();
        // 控制器建立路由约定
        app.MapControllers();

        app.Run();
    }
}
