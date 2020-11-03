using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var seedData = new SeedData(1000, 12);
            modelBuilder.Entity<DALGroup>().HasData(seedData.DALGroups);
            modelBuilder.Entity<DALAppEvent>().HasData(seedData.DALAppEvents);
        }

       
    }
}
