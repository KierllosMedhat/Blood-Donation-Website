using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PL.Models;
using System.Reflection.Metadata;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donor>()
                .HasMany(e => e.Requests)
                .WithOne(e => e.Donor)
                .HasForeignKey(e => e.DonorId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Requests)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Donor>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Donor>(d => d.UserId)
                .HasPrincipalKey<ApplicationUser>(d => d.Id);

            modelBuilder.Entity<Patient>()
                 .HasOne(e => e.User)
                 .WithOne()
                 .HasForeignKey<Patient>(d => d.UserId)
                 .HasPrincipalKey<ApplicationUser>(d => d.Id);

            modelBuilder.Entity<Hospital>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Hospital>(d => d.UserId)
                .HasPrincipalKey<ApplicationUser>(d => d.Id);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<FollowUpForm> FollowUpForms { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
    }
}
