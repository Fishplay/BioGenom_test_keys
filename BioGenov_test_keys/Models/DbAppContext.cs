using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BioGenom_test_keys.Models
{
    public partial class DbAppContext : DbContext
    {
        public DbAppContext()
        {
        }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentMetric> CurrentMetrics { get; set; } = null!;
        public virtual DbSet<NutritionReport> Reports { get; set; } = null!;
        public virtual DbSet<PersonalizedSet> PersonalizedSets { get; set; } = null!;
        public virtual DbSet<SupplementNutrient> SupplementNutrients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;password=root;Database=BioGenomApi");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentMetric>(entity =>
            {
                entity.ToTable("current_metrics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrentValue)
                    .HasPrecision(10, 2)
                    .HasColumnName("current_value");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NormValue).HasColumnName("norm_value");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.CurrentMetrics)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("current_metrics_report_id_fkey");
            });

            modelBuilder.Entity<NutritionReport>(entity =>
            {
                entity.ToTable("nutrition_reports");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ReducedCount).HasColumnName("reduced_count");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SufficientCount).HasColumnName("sufficient_count");
            });

            modelBuilder.Entity<PersonalizedSet>(entity =>
            {
                entity.ToTable("personalized_sets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlternativesCount).HasColumnName("alternatives_count");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.PersonalizedSets)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("personalized_sets_report_id_fkey");
            });

            modelBuilder.Entity<SupplementNutrient>(entity =>
            {
                entity.ToTable("supplement_nutrients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FromNutrition)
                    .HasPrecision(10, 2)
                    .HasColumnName("from_nutrition");

                entity.Property(e => e.FromSet)
                    .HasPrecision(10, 2)
                    .HasColumnName("from_set");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SetId).HasColumnName("set_id");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.SupplementNutrients)
                    .HasForeignKey(d => d.SetId)
                    .HasConstraintName("supplement_nutrients_set_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
