using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.WeatherAggregate.Queries
{
    public class GetWeatherForecastQueryResult : QueryResult<WeatherForecast>
    {
        public GetWeatherForecastQueryResult(WeatherForecast result) : base(result)
        {
        }
    }
}
