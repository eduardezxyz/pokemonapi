using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demoapi.EntityModels
{
    public partial class farmingContext : DbContext
    {
        public farmingContext()
        {
        }

        public farmingContext(DbContextOptions<farmingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crop> Crops { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;
        public virtual DbSet<FarmerType> FarmerTypes { get; set; } = null!;
        public virtual DbSet<FarmerWCropsVw> FarmerWCropsVws { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=farming;User Id=eduard;Password=Cowboys2325_");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crop>(entity =>
            {
                entity.ToTable("crops");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CropName)
                    .HasMaxLength(250)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropSqft).HasColumnName("crop_sqft");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("farmers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FarmerName)
                    .HasMaxLength(250)
                    .HasColumnName("farmer_name");

                entity.Property(e => e.FarmerTypeId).HasColumnName("farmer_type_id");

                entity.Property(e => e.YearsFarming).HasColumnName("years_farming");

                entity.HasOne(d => d.FarmerType)
                    .WithMany(p => p.Farmers)
                    .HasForeignKey(d => d.FarmerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farmers_farmer_type_id_fkey");

                entity.HasMany(d => d.Crops)
                    .WithMany(p => p.Farmers)
                    .UsingEntity<Dictionary<string, object>>(
                        "FarmerCrop",
                        l => l.HasOne<Crop>().WithMany().HasForeignKey("CropId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("farmer_crops_crop_id_fkey"),
                        r => r.HasOne<Farmer>().WithMany().HasForeignKey("FarmerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("farmer_crops_farmer_id_fkey"),
                        j =>
                        {
                            j.HasKey("FarmerId", "CropId").HasName("farmer_crops_pkey");

                            j.ToTable("farmer_crops");

                            j.IndexerProperty<int>("FarmerId").HasColumnName("farmer_id");

                            j.IndexerProperty<int>("CropId").HasColumnName("crop_id");
                        });
            });

            modelBuilder.Entity<FarmerType>(entity =>
            {
                entity.ToTable("farmer_types");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FarmerType1)
                    .HasMaxLength(250)
                    .HasColumnName("farmer_type");
            });

            modelBuilder.Entity<FarmerWCropsVw>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("farmer_w_crops_vw");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CropId).HasColumnName("crop_id");

                entity.Property(e => e.CropName)
                    .HasMaxLength(250)
                    .HasColumnName("crop_name");

                entity.Property(e => e.CropSqft).HasColumnName("crop_sqft");

                entity.Property(e => e.FarmerName)
                    .HasMaxLength(250)
                    .HasColumnName("farmer_name");

                entity.Property(e => e.FarmerType)
                    .HasMaxLength(250)
                    .HasColumnName("farmer_type");

                entity.Property(e => e.FarmerTypeId).HasColumnName("farmer_type_id");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
