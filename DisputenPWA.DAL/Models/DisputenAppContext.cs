using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.WeatherAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.DAL.Models
{
    public class DisputenAppContext : DbContext
    {
        public DisputenAppContext(DbContextOptions<DisputenAppContext> options)
           : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Group> Groups{ get; set; }
    }
}
