namespace Mtc.Host.WebApi.TimeTask;
/// <summary>
/// JobTask
/// </summary>
public class JobTaskService : BackgroundService
{
    private Timer indexTimer;
    /// <summary>
    /// JobTask
    /// </summary>        
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        indexTimer = new Timer(RefreshIndex, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }
    private void RefreshIndex(object state)
    {
        GC.Collect();
    }
    public override void Dispose()
    {
        base.Dispose();
        indexTimer?.Dispose();
    }
}
