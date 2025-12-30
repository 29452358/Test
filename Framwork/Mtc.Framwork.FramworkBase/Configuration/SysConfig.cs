
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Mtc.Framwork.FramworkBase.Configuration;
public class SysConfig
{
    JsonDocument doc;
    object o = new object();
    ConcurrentDictionary<string, string> config = new();
    public SysConfig(string config)
    {
        var path = Directory.GetCurrentDirectory() + "\\" + config;
        string json = GetFile(path);
        json = RemoveComments(json);
        doc = JsonDocument.Parse(json);
        foreach (var item in doc.RootElement.EnumerateObject())
        {
            JsonRoot(item.Value, item.Name, item.Value.ToString());
        }
    }
    public void JsonRoot(JsonElement json, string name, string value)
    {
        if (json.ValueKind == JsonValueKind.String)
        {
            config[name] = value;
            return;
        }
        foreach (var item in json.EnumerateObject())
        {
            string key = $"{name}:{item.Name}";
            JsonRoot(item.Value, key, item.Value.ToString());
        }
    }
    public string GetFile(string path)
    {
        lock (o)
        {
            using (FileStream file = new FileStream(path, FileMode.Open))
            {
                byte[] data = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(data, 0, (int)file.Length);
                return Encoding.UTF8.GetString(data);
            }
        }
    }
    // 移除 // 单行注释
    public string RemoveComments(string jsonWithComments)
    {
        // 移除 // 注释
        string pattern = @"///.*?$";
        string result = Regex.Replace(jsonWithComments, pattern, "", RegexOptions.Multiline);
        return result;
    }
    public string Get(string key)
    {
        if (config.TryGetValue(key, out string value))
        {
            return value;
        }
        return value;
    }
    public string Set(string key, string value)
    {
        return config[key] = value;
    }
    /// <summary>
    /// KeyALL
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<string, string>> GetAll()
    {
        return config.AsEnumerable();
    }
}
