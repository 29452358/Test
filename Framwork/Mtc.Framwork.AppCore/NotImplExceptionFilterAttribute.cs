using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Mtc.Framwork.FramworkBase.Utility;
using Mtc.Framwork.FramworkBase;

namespace Mtc.Framwork.AppCore;
/// <summary>
/// 统一错误处理
/// </summary>
public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// 统一错误处理
    /// </summary>
    /// <param name="context"></param>
    public override async void OnException(ExceptionContext context)
    {
        //取得发生例外时的错误讯息
        string errorMessages = context.Exception.Message;
        string errorStackTrace = context.Exception.StackTrace;
        //异常已处理了
        context.ExceptionHandled = true;
        //返回
        context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
        context.HttpContext.Response.StatusCode = 200;
        try
        {
            await context.HttpContext.Response.WriteAsync(WebApiResult.SystemError(errorMessages, errorStackTrace).ToJson());
            //统一系统日志
            LogHandler.Error(errorMessages);
            LogHandler.Error(errorStackTrace);
        }
        catch { }
        base.OnException(context);
    }
}
