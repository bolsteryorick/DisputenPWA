using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
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
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<DALGroup> Groups{ get; set; }
        public DbSet<DALAppEvent> AppEvents { get; set; }
    }
}
