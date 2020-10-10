using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.WeatherAggregate.Commands
{
    public class SetWeatherForecastCommandResult : CommandResult<WeatherForecast>
    {
        public SetWeatherForecastCommandResult(WeatherForecast result) : base(result)
        {
        }
    }
}
