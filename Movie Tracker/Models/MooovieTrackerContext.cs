using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Movie_Tracker.Models
{
    public partial class MooovieTrackerContext : DbContext
    {
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Compozitor> Compozitor { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmActor> FilmActor { get; set; }
        public virtual DbSet<FilmProducator> FilmProducator { get; set; }
        public virtual DbSet<Producator> Producator { get; set; }
        public virtual DbSet<Regizor> Regizor { get; set; }
        public virtual DbSet<Scenarist> Scenarist { get; set; }
        public virtual DbSet<UserFilm> UserFilm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MooovieTracker;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.IdActor);

                entity.Property(e => e.IdActor).ValueGeneratedNever();

                entity.Property(e => e.Nume).HasMaxLength(256);

                entity.Property(e => e.Prenume).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Compozitor>(entity =>
            {
                entity.HasKey(e => e.IdCompozitor);

                entity.Property(e => e.IdCompozitor).ValueGeneratedNever();

                entity.Property(e => e.Nume).HasMaxLength(256);

                entity.Property(e => e.Prenume).HasMaxLength(256);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.IdFilm);

                entity.Property(e => e.IdFilm).ValueGeneratedNever();

                entity.Property(e => e.Durata).HasMaxLength(256);

                entity.Property(e => e.Gen).HasMaxLength(256);

                entity.HasOne(d => d.IdCompozitorNavigation)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.IdCompozitor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Film_IdCompozitor");

                entity.HasOne(d => d.IdRegizorNavigation)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.IdRegizor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Film_IdRegizor");

                entity.HasOne(d => d.IdScenaristNavigation)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.IdScenarist)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Film_IdScenarist");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => e.IdFilmActor);

                entity.Property(e => e.IdFilmActor).ValueGeneratedNever();

                entity.HasOne(d => d.IdActorNavigation)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.IdActor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FilmActor_IdActor");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.IdFilm)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FilmActor_IdFilm");
            });

            modelBuilder.Entity<FilmProducator>(entity =>
            {
                entity.HasKey(e => e.IdFilmProducator);

                entity.Property(e => e.IdFilmProducator).ValueGeneratedNever();

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany(p => p.FilmProducator)
                    .HasForeignKey(d => d.IdFilm)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FilmProducator_IdFilm");

                entity.HasOne(d => d.IdProducatorNavigation)
                    .WithMany(p => p.FilmProducator)
                    .HasForeignKey(d => d.IdProducator)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FilmProducator_IdProducator");
            });

            modelBuilder.Entity<Producator>(entity =>
            {
                entity.HasKey(e => e.IdProducator);

                entity.Property(e => e.IdProducator).ValueGeneratedNever();

                entity.Property(e => e.Nume).HasMaxLength(256);

                entity.Property(e => e.Prenume).HasMaxLength(256);
            });

            modelBuilder.Entity<Regizor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nume).HasMaxLength(256);

                entity.Property(e => e.Prenume).HasMaxLength(256);
            });

            modelBuilder.Entity<Scenarist>(entity =>
            {
                entity.HasKey(e => e.IdScenarist);

                entity.Property(e => e.IdScenarist).ValueGeneratedNever();

                entity.Property(e => e.Nume).HasMaxLength(256);

                entity.Property(e => e.Prenume).HasMaxLength(256);
            });

            modelBuilder.Entity<UserFilm>(entity =>
            {
                entity.HasKey(e => e.IdUserFilm);

                entity.Property(e => e.IdUserFilm).ValueGeneratedNever();

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany(p => p.UserFilm)
                    .HasForeignKey(d => d.IdFilm)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserFilm_IdFilm");
            });
        }
    }
}
