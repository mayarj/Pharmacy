using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Infrastructure.Models
{
    public partial class PharmacyContext : IdentityDbContext<User,IdentityRole,string>
    {
        public PharmacyContext()
        {
        }

        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Factory> Factories { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Medicine> Medicines { get; set; } = null!;
        public virtual DbSet<MedicineIngredient> MedicineIngredients { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
        public virtual DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Data Source=DESKTOP-LP74TAC\\GOD;Initial Catalog=Pharmacy;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "IX_Category")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Factory>(entity =>
            {
                entity.ToTable("Factory");

                entity.HasIndex(e => e.Name, "IX_Factory")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.HasIndex(e => e.Name, "IX_Ingredient")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.HasIndex(e => e.TradeName, "IX_Medicine")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Dose).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TradeName).HasMaxLength(50);

                entity.HasOne(d => d.ActiveSubstance)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.ActiveSubstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicine_Ingredient");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicine_Category");

                entity.HasOne(d => d.Factory)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.FactoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicine_Factory");
            });

            modelBuilder.Entity<MedicineIngredient>(entity =>
            {
                entity.ToTable("MedicineIngredient");

                entity.HasIndex(e => new { e.IngredientId, e.MedicineId }, "IX_MedicineIngredient")
                    .IsUnique();

                entity.Property(e => e.Ratio).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicineIngredient_Ingredient");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.MedicineId)
                    .HasConstraintName("FK_MedicineIngredient_Medicine");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMedicine_Patient");
            });

            modelBuilder.Entity<PrescriptionMedicine>(entity =>
            {
                entity.ToTable("PrescriptionMedicine");

                entity.HasIndex(e => new { e.MedicineId, e.PrescriptionId }, "IX_PrescriptionMedicine")
                    .IsUnique();

                entity.Property(e => e.Count).HasColumnName("count");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.PrescriptionMedicines)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionMedicine_Medicine");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionMedicines)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionMedicine_Prescription");
            }); 

           
            

            //OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
