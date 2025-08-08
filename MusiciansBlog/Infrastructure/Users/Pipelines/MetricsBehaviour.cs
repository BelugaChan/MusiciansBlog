using MediatR;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace MusiciansBlog.API.Infrastructure.Users.Pipelines
{
    public class MetricsBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Counter<double> _counter;
        public MetricsBehaviour()
        {
            _counter = new Meter("MyApp.BusinessMetrics")
                .CreateCounter<double>("database.processing.time", "ms");
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                return await next(cancellationToken);
            }
            finally
            {
                timer.Stop();
                _counter.Add(
                    timer.ElapsedMilliseconds,
                    new KeyValuePair<string, object?>("handler", typeof(TRequest).Name));
            }
        }
    }
}
