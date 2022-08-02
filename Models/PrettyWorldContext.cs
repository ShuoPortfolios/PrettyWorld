using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrettyWorld.Models
{
    public partial class PrettyWorldContext : DbContext
    {
        public PrettyWorldContext()
        {
        }

        public PrettyWorldContext(DbContextOptions<PrettyWorldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieTypeList> MovieTypeLists { get; set; } = null!;
        public virtual DbSet<MyProfile> MyProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=PrettyWorld;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieName);

                entity.Property(e => e.MovieName).HasMaxLength(50);

                entity.Property(e => e.Acting).HasDefaultValueSql("((0))");

                entity.Property(e => e.Director).HasMaxLength(255);

                entity.Property(e => e.Immersion).HasDefaultValueSql("((0))");

                entity.Property(e => e.MovieId).ValueGeneratedOnAdd();

                entity.Property(e => e.MoviePicture)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MovieType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Plot).HasDefaultValueSql("((0))");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(2, 1)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Scene).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sound).HasDefaultValueSql("((0))");

                entity.Property(e => e.Trailer)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WatchDate).HasColumnType("date");
            });

            modelBuilder.Entity<MovieTypeList>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("MovieTypeList");

                entity.Property(e => e.TypeName).HasMaxLength(10);
            });

            modelBuilder.Entity<MyProfile>(entity =>
            {
                entity.ToTable("MyProfile");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Ajaxlevel).HasColumnName("AJAXLevel");

                entity.Property(e => e.CsharpLevel).HasColumnName("CSharpLevel");

                entity.Property(e => e.Csslevel).HasColumnName("CSSLevel");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FacebookUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.GithubUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Htmllevel).HasColumnName("HTMLLevel");

                entity.Property(e => e.InstagramUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Introduction).HasMaxLength(500);

                entity.Property(e => e.LiveIn).HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Mssqllevel).HasColumnName("MSSQLLevel");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
