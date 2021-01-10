using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
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
        public DbSet<DalAttendee> Attendees { get; set; }
        public DbSet<DalPlatformContact> PlatformContacts { get; set; }
        public DbSet<DalOutsideContact> OutsideContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DalMember>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Members);

            modelBuilder.Entity<DalMember>()
                .HasOne(x => x.User)
                .WithMany(x => x.GroupMemberships);

            modelBuilder.Entity<DalAppEvent>()
                .HasOne(x => x.Group)
                .WithMany(x => x.AppEvents);

            modelBuilder.Entity<DalAttendee>()
                .HasOne(x => x.User)
                .WithMany(x => x.Attendences);

            modelBuilder.Entity<DalAttendee>()
                .HasOne(x => x.AppEvent)
                .WithMany(x => x.Attendances);

            modelBuilder.Entity<DalPlatformContact>()
                .HasOne(x => x.User)
                .WithMany(x => x.PlatformContacts);

            modelBuilder.Entity<DalPlatformContact>()
                .HasOne(x => x.ContactUser)
                .WithMany(x => x.PlatformContactReferences);

            modelBuilder.Entity<DalOutsideContact>()
                .HasOne(x => x.User)
                .WithMany(x => x.OutsideContacts);

            modelBuilder.Entity<DalAppEvent>().HasIndex(a => a.GroupId);
            modelBuilder.Entity<DalMember>().HasIndex(a => a.GroupId);
            modelBuilder.Entity<DalMember>().HasIndex(a => a.UserId);
            modelBuilder.Entity<DalAttendee>().HasIndex(a => a.AppEventId);
            modelBuilder.Entity<DalPlatformContact>().HasIndex(a => a.UserId);
            modelBuilder.Entity<DalPlatformContact>().HasIndex(a => a.ContactUserId);

            base.OnModelCreating(modelBuilder);
        }  
    }
}
