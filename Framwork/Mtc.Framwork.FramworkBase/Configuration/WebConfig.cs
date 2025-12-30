

namespace Mtc.Framwork.FramworkBase.Configuration;
/// <summary>
/// 配置中心
/// </summary>
public class WebConfig
{
    static SysConfig configHelper = new("appsettings.json");
    public static void Builder()
    {
        configHelper = new("appsettings.json");
    }
    /// <summary>
    /// Key:Value Key = Logging:LogLevel:Default
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetSection(string key)
    {
        return configHelper.Get(key);
    }
    /// <summary>
    /// Logging:LogLevel:Default = "123";
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string SetSection(string key, string value)
    {
        return configHelper.Set(key, value);
    }
    /// <summary>
    /// Del Key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static void DelSection(string key)
    {
        configHelper.Set(key, null);
    }
    /// <summary>
    /// KeyALL
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IEnumerable<KeyValuePair<string, string>> GetAll()
    {
        return configHelper.GetAll();
    }
}
