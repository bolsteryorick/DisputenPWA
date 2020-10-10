using DisputenPWA.Domain.SeedWorks.Cqrs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.WeatherAggregate.Commands
{
    public class SetWeatherForecastCommand : IRequest<SetWeatherForecastCommandResult>
    {
        public string Summary { get; }

        public SetWeatherForecastCommand(string summary)
        {
            Summary = summary;
        }
    }
}
