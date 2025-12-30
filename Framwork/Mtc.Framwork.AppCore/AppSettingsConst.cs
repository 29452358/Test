
using Mtc.Framwork.FramworkBase.Configuration;

namespace Mtc.Framwork.AppCore;
public class AppSettingsConst
{
    public static readonly string CorsPolicyName = WebConfig.GetSection("Cors:PolicyName");
    public static readonly bool CorsEnableAllIP = ObjToBool(WebConfig.GetSection("Cors:EnableAllIP"));
    public static readonly string CorsIP = WebConfig.GetSection("Cors:IP");
    public static bool ObjToBool(object thisValue)
    {
        bool result = false;
        if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out result))
        {
            return result;
        }
        return result;
    }
}
