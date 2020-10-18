using DisputenPWA.Domain.WeatherAggregate.Queries;
using DisputenPWA.Infrastructure.SqlDatabase;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Weather.Handlers
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryResult>
    {
        private readonly ISqlDatabaseConnector _sqlDatabaseConnector;

        public GetWeatherForecastHandler(
            ISqlDatabaseConnector sqlDatabaseConnector
            )
        {
            _sqlDatabaseConnector = sqlDatabaseConnector;
        }

        public async Task<GetWeatherForecastQueryResult> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var date = Convert.ToDateTime(request.Date);
            var forecast = await _sqlDatabaseConnector.GetForecastOnDay(date);
            return new GetWeatherForecastQueryResult(forecast);
        }
    }
}