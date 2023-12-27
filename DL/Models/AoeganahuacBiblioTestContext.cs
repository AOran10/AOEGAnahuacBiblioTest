using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL.Models;

public partial class AoeganahuacBiblioTestContext : DbContext
{
    public AoeganahuacBiblioTestContext()
    {
    }

    public AoeganahuacBiblioTestContext(DbContextOptions<AoeganahuacBiblioTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Medio> Medios { get; set; }

    public virtual DbSet<TipoMedio> TipoMedios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= AOEGAnahuacBiblioTest; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__DD33B031D34E2C34");

            entity.ToTable("Autor");

            entity.Property(e => e.InformacionAdicional)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PK__Editoria__EF8386718EF45D9B");

            entity.ToTable("Editorial");

            entity.Property(e => e.InformacionAdicional)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988198A3EED");

            entity.ToTable("Genero");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma).HasName("PK__Idioma__C867BD36B1757A73");

            entity.ToTable("Idioma");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Medio>(entity =>
        {
            entity.HasKey(e => e.IdMedio).HasName("PK__Medio__EF8018608060578B");

            entity.ToTable("Medio");

            entity.Property(e => e.Publicacion).HasColumnType("date");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK__Medio__IdAutor__20C1E124");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK__Medio__IdEditori__1ED998B2");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__Medio__IdGenero__21B6055D");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK__Medio__IdIdioma__1FCDBCEB");

            entity.HasOne(d => d.IdTipoMedioNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdTipoMedio)
                .HasConstraintName("FK__Medio__IdTipoMed__1DE57479");
        });

        modelBuilder.Entity<TipoMedio>(entity =>
        {
            entity.HasKey(e => e.IdTipoMedio).HasName("PK__TipoMedi__7A9964B23F7E81D7");

            entity.ToTable("TipoMedio");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
