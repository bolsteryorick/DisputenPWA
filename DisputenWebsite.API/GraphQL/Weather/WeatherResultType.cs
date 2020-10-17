using DisputenPWA.Domain.WeatherAggregate;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Weather
{
    public class WeatherResultType : ObjectGraphType<WeatherForecast>
    {
        public WeatherResultType()
        {
            Field(s => s.Date).Description("Indicates what day the wheather forecast is for.");
            Field(s => s.TemperatureC).Description("The temperature in degree celcius.");
            Field(s => s.TemperatureF).Description("The temperature in degree fahrenheit.");
            Field(s => s.Summary).Description("A short summary to describe the weather.");
        }
    }
}
