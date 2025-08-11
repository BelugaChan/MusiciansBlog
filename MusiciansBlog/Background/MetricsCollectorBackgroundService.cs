
using StackExchange.Redis;
using System.Diagnostics.Metrics;

namespace MusiciansBlog.API.Background
{
    public class MetricsCollectorBackgroundService : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly Gauge<int> _keysGauge;

        public MetricsCollectorBackgroundService(
            IMeterFactory meterFactory, 
            IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            var meter = meterFactory.Create("myapp.redis");
            _keysGauge = meter.CreateGauge<int>("keys.count");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = _connectionMultiplexer.GetDatabase();
                var server = _connectionMultiplexer.GetServer(
                    _connectionMultiplexer.GetEndPoints().First());

                int count = server.Keys().Count();

                _keysGauge.Record(count);

                await Task.Delay(30_000);
            }
        }
    }
}
