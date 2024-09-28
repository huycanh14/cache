using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AspApi_Caching.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMemoryCache _memoryCache;
    public string cacheKey = "Summaries";


    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IMemoryCache memoryCache
    )
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        List<WeatherForecast> data;
        _logger.LogInformation("Start get data");
        if(!_memoryCache.TryGetValue(cacheKey, out data))
        {
            _logger.LogInformation("Add data to memory");
            data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
     
            _memoryCache.Set(cacheKey, data,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(2)));
        }
        _logger.LogInformation("Return data");
        return data;
    }
}
