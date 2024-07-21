using System.Diagnostics.Metrics;

namespace OpenTelemetryDemo.ApiService;

public class ForecastMetrics
{
    private readonly Counter<int> _forecastsGenerated;

    public ForecastMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("Forecasts.Store");
        _forecastsGenerated = meter.CreateCounter<int>("forecasts.store.forecasts_generated");
    }

    public void ForecastsGenerated(int quantity)
    {
        _forecastsGenerated.Add(quantity);
    }
}