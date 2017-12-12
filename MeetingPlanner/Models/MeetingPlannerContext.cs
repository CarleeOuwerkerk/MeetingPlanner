using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeetingPlanner.Models
{
    public partial class MeetingPlannerContext : DbContext
    {
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<Speaker> Speaker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MeetingPlanner;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.Property(e => e.MeetingId).HasColumnName("Meeting_ID");

                entity.Property(e => e.ClosingHymn)
                    .IsRequired()
                    .HasColumnName("Closing_Hymn");

                entity.Property(e => e.ClosingPrayer)
                    .IsRequired()
                    .HasColumnName("Closing_Prayer");

                entity.Property(e => e.Conductor).IsRequired();

                entity.Property(e => e.IntermediateHymn)
                    .IsRequired()
                    .HasColumnName("Intermediate_Hymn");

                entity.Property(e => e.OpeningHymn)
                    .IsRequired()
                    .HasColumnName("Opening_Hymn");

                entity.Property(e => e.OpeningPrayer)
                    .IsRequired()
                    .HasColumnName("Opening_Prayer");

                entity.Property(e => e.SacramentHymn)
                    .IsRequired()
                    .HasColumnName("Sacrament_Hymn");
            });

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.Property(e => e.SpeakerId)
                    .HasColumnName("Speaker_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MeetingId).HasColumnName("Meeting_ID");

                entity.Property(e => e.Subject).IsRequired();

                entity.HasOne(d => d.SpeakerNavigation)
                    .WithOne(p => p.Speaker)
                    .HasForeignKey<Speaker>(d => d.SpeakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Speaker_Meeting");
            });
        }
    }
}
