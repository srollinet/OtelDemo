using System.Diagnostics;

namespace OpenTelemetryDemo.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    
    private static readonly ActivitySource RegisteredActivity = new ActivitySource("Examples.ManualInstrumentations.ForecastProcessing");
    
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10,
        CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = null;

        var count = 0;
        await foreach (var forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast",
                           cancellationToken))
        {
            using (RegisteredActivity.StartActivity($"Processing forecast {count++}"))
            {
                if (forecasts?.Count >= maxItems)
                {
                    break;
                }

                if (forecast is not null)
                {
                    forecasts ??= [];
                    forecasts.Add(forecast);
                }
            }
        }

        return forecasts?.ToArray() ?? [];
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}