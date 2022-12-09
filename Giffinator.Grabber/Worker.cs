namespace Giffinator.Grabber;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private TimeSpan _period = TimeSpan.FromSeconds(10);

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            //GRAD!
        }
    }
}