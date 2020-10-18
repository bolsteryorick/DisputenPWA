using DisputenPWA.Domain.WeatherAggregate;
using DisputenPWA.Domain.WeatherAggregate.Commands;
using DisputenPWA.Infrastructure.SqlDatabase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Weather.Handlers
{
    public class SetWheatherForecastHandler : IRequestHandler<SetWeatherForecastCommand, SetWeatherForecastCommandResult>
    {
        private readonly ISqlDatabaseConnector _sqlDatabaseConnector;

        public SetWheatherForecastHandler(
            ISqlDatabaseConnector sqlDatabaseConnector
            )
        {
            _sqlDatabaseConnector = sqlDatabaseConnector;
        }

        public async Task<SetWeatherForecastCommandResult> Handle(SetWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(rng.Next(1, 7)),
                TemperatureC = rng.Next(-20, 55),
                Summary = request.Summary
            };
            await _sqlDatabaseConnector.Add(forecast);
            return new SetWeatherForecastCommandResult(forecast);
        }
    }
}
