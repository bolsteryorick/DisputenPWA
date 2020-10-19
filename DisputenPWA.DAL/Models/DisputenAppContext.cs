using DisputenPWA.Domain.GroupAggregate;
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

        public DbSet<Group> Groups{ get; set; }
    }
}
