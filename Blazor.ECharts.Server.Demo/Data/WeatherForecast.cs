namespace Blazor.ECharts.Server.Demo.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public record WeatherJSON
    {
        public Forecast24hData? data { get; set; }
        public string? message { get; set; }
        public int status { get; set; }
    }

    public record Forecast24hData
    {
        public Dictionary<string, Forecast24h>? forecast_24h { get; set; }
    }

    public record Forecast24h
    {
        // ����API���ص����ݽṹ�������Ӧ������
        public string? time { get; set; }
        public string? day_weather { get; set; }
        public string? day_wind_direction { get; set; }
        public string? day_wind_power { get; set; }
        public string? max_degree { get; set; }
        public string? night_weather { get; set; }
        public string? night_wind_direction { get; set; }
        public string? night_wind_power { get; set; }
        public string? min_degree { get; set; }
    }
}