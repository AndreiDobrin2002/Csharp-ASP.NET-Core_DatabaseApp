using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ProgramareMedic.Models
{
    public partial class ProgramareMedicContext : DbContext
    {
        public ProgramareMedicContext(DbContextOptions<ProgramareMedicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pacienti> Pacienti { get; set; }
        public virtual DbSet<Medici> Medici { get; set; }
        public virtual DbSet<Programari> Programari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacienti>(entity =>
            {
                entity.ToTable("Pacienti");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nume)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prenume)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Varsta)
                    .IsRequired();

                entity.Property(e => e.Adresa)
                    .IsRequired();

                entity.HasCheckConstraint("CK_Varsta", "Varsta >= 0");
                entity.HasCheckConstraint("CK_Nume", "LEN(Nume) > 0");
                entity.HasCheckConstraint("CK_Prenume", "LEN(Prenume) > 0");
                entity.HasCheckConstraint("CK_Adresa", "LEN(Adresa) > 0");
            });

            modelBuilder.Entity<Medici>(entity =>
            {
                entity.ToTable("Medici");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NumeMedic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrenumeMedic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Specializare)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Clinica)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasCheckConstraint("CK_Nume", "LEN(NumeMedic) > 0");
                entity.HasCheckConstraint("CK_Prenume", "LEN(PrenumeMedic) > 0");
            });

            modelBuilder.Entity<Programari>(entity =>
            {
                entity.ToTable("Programari");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DataProgramare)
                    .IsRequired();

                entity.Property(e => e.CategorieServicii)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasCheckConstraint("CK_DataProgramare", "DataProgramare > GETDATE()");
                entity.HasCheckConstraint("CK_CategorieServicii", "LEN(CategorieServicii) > 0");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.Programari)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Programari_Pacienti");

                entity.HasOne(d => d.Medic)
                    .WithMany(m => m.Programari)
                    .HasForeignKey(d => d.MedicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Programari_Medici");
            });
        }
    }
}
