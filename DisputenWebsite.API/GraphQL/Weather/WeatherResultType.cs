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
            Field(f => f.Date).Description("Indicates what day the wheather forecast is for.");
            Field(f => f.TemperatureC).Description("The temperature in degree celcius.");
            Field(f => f.TemperatureF).Description("The temperature in degree fahrenheit.");
            Field(f => f.Summary).Description("A short summary to describe the weather.");
        }
    }
}
