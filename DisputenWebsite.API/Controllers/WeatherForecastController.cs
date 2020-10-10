using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisputenPWA.Domain.WeatherAggregate;
using DisputenPWA.Domain.WeatherAggregate.Commands;
using DisputenPWA.Domain.WeatherAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DisputenWebsite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<WeatherForecast> Get()
        {
            return (await _mediator.Send(new GetWeatherForecastQuery())).Result;
        }

        [Route("{summary}")]
        [HttpPost]
        public async Task<WeatherForecast> Post(string summary)
        {
            return (await _mediator.Send(new SetWeatherForecastCommand(summary))).Result;
        }
    }
}
