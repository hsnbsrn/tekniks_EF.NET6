using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace net6d1.Models
{
    public partial class teknikContext : DbContext
    {
        public teknikContext()
        {
        }

        public teknikContext(DbContextOptions<teknikContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Durum> Durums { get; set; } = null!;
        public virtual DbSet<Islem> Islems { get; set; } = null!;
        public virtual DbSet<Mcihaz> Mcihazs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Durum>(entity =>
            {
                entity.ToTable("durum");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .HasColumnName("ad");
            });

            modelBuilder.Entity<Islem>(entity =>
            {
                entity.ToTable("islem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ad).HasMaxLength(50);

                entity.Property(e => e.Ucret);

            });

            modelBuilder.Entity<Mcihaz>(entity =>
            {
                entity.ToTable("mcihaz");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdSoyad).HasMaxLength(50);

                entity.Property(e => e.Btarih)
                    .HasColumnType("datetime")
                    .HasColumnName("BTarih");

                entity.Property(e => e.Cihaz).HasColumnType("text");

                entity.Property(e => e.DetayIslem).HasColumnType("text");

                entity.Property(e => e.Getirilen).HasColumnType("text");

                entity.Property(e => e.Kod)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.TelNo).HasMaxLength(11);

                entity.Property(e => e.Ucret);
                
                entity.Property(e => e.Persad).HasMaxLength(50);

                entity.Property(e => e.Vtarih)
                    .HasColumnType("datetime")
                    .HasColumnName("VTarih");

                entity.HasOne(d => d.DurumNavigation)
                    .WithMany(p => p.Mcihazs)
                    .HasForeignKey(d => d.Durum)
                    .HasConstraintName("FK_mcihaz_durum");

                entity.HasOne(d => d.IslemNavigation)
                    .WithMany(p => p.Mcihazs)
                    .HasForeignKey(d => d.Islem)
                    .HasConstraintName("FK_mcihaz_islem");
                
                entity.HasOne(d => d.PersonelNavigation)
                    .WithMany(p => p.Mcihazs)
                    .HasForeignKey(d => d.Personel)
                    .HasConstraintName("FK_mcihaz_personel");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
