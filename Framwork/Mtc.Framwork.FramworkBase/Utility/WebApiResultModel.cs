namespace Mtc.Framwork.FramworkBase.Utility;
public class WebApiResultModel
{
    /// <summary>
    /// 0 成功 1 失败
    /// </summary>
    public int Code { get; set; } = 1;
    /// <summary>
    /// Data
    /// </summary>
    public object Data { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    public string Msg { get; set; }
    /// <summary>
    /// 特殊消息
    /// </summary>
    public string SpecialMsg { get; set; }
    /// <summary>
    /// 总数
    /// </summary>
    public int Count { get; set; }
}
public class WebApiResultModel<T>
{
    /// <summary>
    /// 0 成功 1 失败
    /// </summary>
    public int Code { get; set; } = 1;
    /// <summary>
    /// Data
    /// </summary>
    public T Result { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    public string Msg { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string SpecialMsg { get; set; }
    /// <summary>
    /// 总数
    /// </summary>
    public int Count { get; set; }
}
