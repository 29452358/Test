
using System.Text.Json;

namespace Mtc.Framwork.FramworkBase;
public static class JsonHelper
{
    /// <summary>
    /// json转Class  ByteExtended
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static T ToClass<T>(this string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 或者 JsonNamingPolicy.SnakeCase 等，根据需要选择合适的策略
            PropertyNameCaseInsensitive = true // 反序列化时忽略大小写
        };
        return json == null ? default : JsonSerializer.Deserialize<T>(json, options);
    }
    /// <summary>
    /// json转Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static object ToClass(this string json, Type type)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 或者 JsonNamingPolicy.SnakeCase 等，根据需要选择合适的策略
            PropertyNameCaseInsensitive = true // 反序列化时忽略大小写
        };
        return json == null ? null : JsonSerializer.Deserialize(json, type, options);
    }
    /// <summary>
    /// 转换为json
    /// </summary>
    /// <param name="jsonObject"></param>
    /// <returns></returns>
    public static string ToJson(this object jsonObject)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 或者 JsonNamingPolicy.SnakeCase 等，根据需要选择合适的策略
            PropertyNameCaseInsensitive = true // 反序列化时忽略大小写
        };
        return JsonSerializer.Serialize(jsonObject, options);
    }
}
