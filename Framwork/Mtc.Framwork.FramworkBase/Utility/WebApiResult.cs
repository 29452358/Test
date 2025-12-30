namespace Mtc.Framwork.FramworkBase.Utility;
public class WebApiResult
{
    public static WebApiResultModel Success(object o, int count = 0, string msg = "ok", string specialMsg = "ok")
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 0;
        webApiResultModel.Data = o;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = specialMsg;
        webApiResultModel.Count = count;
        return webApiResultModel;
    }
    public static WebApiResultModel Success()
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 0;
        webApiResultModel.Data = null;
        webApiResultModel.Msg = "ok";
        webApiResultModel.Count = 0;
        return webApiResultModel;
    }
    public static WebApiResultModel Warning(string msg, object o = null, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 1;
        webApiResultModel.Data = o;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel Error(string msg, object o, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 2;
        webApiResultModel.Data = o;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel Error(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 2;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel Fatal404(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 404;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel Fatal(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 500;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel SystemError(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 4;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel NoLogin(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 401;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        return webApiResultModel;
    }
    public static WebApiResultModel NoPermission(string msg, string smsg = null)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = 6;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = msg;
        return webApiResultModel;
    }
    public static WebApiResultModel Message(int code, string msg, string smsg = null, object o = null, int count = 0)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = code;
        webApiResultModel.Msg = msg;
        webApiResultModel.SpecialMsg = smsg;
        webApiResultModel.Data = o;
        webApiResultModel.Count = count;
        return webApiResultModel;
    }
    public static WebApiResultModel Copy<T>(WebApiResultModel<T> model)
    {
        WebApiResultModel webApiResultModel = new();
        webApiResultModel.Code = model.Code;
        webApiResultModel.Data = model.Result;
        webApiResultModel.Msg = model.Msg;
        webApiResultModel.Count = model.Count;
        webApiResultModel.SpecialMsg = model.SpecialMsg;
        return webApiResultModel;
    }
}
