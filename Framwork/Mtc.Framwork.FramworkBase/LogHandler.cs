
using NLog;

namespace Mtc.Framwork.FramworkBase;
public class LogHandler
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    public static void Debug(string msg, params object[] args)
    {
        logger.Debug(msg, args);
    }
    public static void Info(string msg, params object[] args)
    {
        logger.Info(msg, args);
    }
    public static void Warn(string msg, params object[] args)
    {
        logger.Warn(msg, args);
    }
    public static void Trace(string msg, params object[] args)
    {
        logger.Trace(msg, args);
    }
    public static void Error(string msg, params object[] args)
    {
        logger.Error(msg, args);
    }
    public static void Fatal(string msg, params object[] args)
    {
        logger.Fatal(msg, args);
    }
}
