using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Domain.GroupAggregate.DalObject;
using DisputenPWA.Domain.MemberAggregate.DalObject;
using DisputenPWA.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisputenPWA.DAL.Models
{
    public class DisputenAppContext : IdentityDbContext<ApplicationUser>
    {
        public DisputenAppContext(DbContextOptions<DisputenAppContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<DalGroup> Groups{ get; set; }
        public DbSet<DalAppEvent> AppEvents { get; set; }
        public DbSet<DalMember> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //var seedData = new SeedData(100, 12);
            //modelBuilder.Entity<DalGroup>().HasData(seedData.DALGroups);
            //modelBuilder.Entity<DalAppEvent>().HasData(seedData.DALAppEvents);
            modelBuilder.Entity<DalMember>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Members);

            modelBuilder.Entity<DalMember>()
                .HasOne(x => x.User)
                .WithMany(x => x.GroupMemberships);

            base.OnModelCreating(modelBuilder);
        }

       
    }
}
