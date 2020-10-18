using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.WeatherAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.SqlDatabase
{
    public interface ISqlDatabaseConnector
    {
        Task Add(WeatherForecast weatherForecast);
        Task<WeatherForecast> GetForecastOnDay(DateTime dateTime);
    }

    public class SqlDatabaseConnector : ISqlDatabaseConnector
    {
        private readonly IRepository<WeatherForecast> _weatherForecastRepository;

        public SqlDatabaseConnector(
            IRepository<WeatherForecast> weatherForecastRepository
            )
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public Task<WeatherForecast> GetForecastOnDay(DateTime dateTime)
        {
            var queryable = _weatherForecastRepository.GetQueryable();
            return queryable.FirstOrDefaultAsync(x => x.Date.DayOfYear == dateTime.DayOfYear);
        }

        public async Task Add(WeatherForecast weatherForecast)
        {
            await _weatherForecastRepository.Add(weatherForecast);
        }
    }
}
