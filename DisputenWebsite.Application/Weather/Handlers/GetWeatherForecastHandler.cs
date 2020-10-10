using DisputenPWA.Domain.WeatherAggregate;
using DisputenPWA.Domain.WeatherAggregate.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Weather.Handlers
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryResult>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<GetWeatherForecastQueryResult> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            // todo add async database call
            var rng = new Random();
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(rng.Next(1,7)),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
            return new GetWeatherForecastQueryResult(forecast);
        }
    }
}