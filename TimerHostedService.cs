using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using DB2VM;
using Basic;
public class TimerHostedService : IHostedService, IDisposable
{
    private Timer timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // 設定 Timer 的間隔時間（以毫秒為單位）
        int interval = CalculateNextExecutionTime();

        // 創建 Timer 實例
        timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(interval));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {

        // 在這裡執行您需要的任務邏輯
        // ...
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    // 計算下一次執行時間的方法
    private int CalculateNextExecutionTime()
    {
        int interval = 10000;
        // 根據需求計算下一次執行時間的間隔，例如每天的特定時間、每小時等等
        // ...

        // 返回下一次執行時間的間隔（以毫秒為單位）
        return interval;
    }
}
