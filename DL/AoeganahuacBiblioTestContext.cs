using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class AoeganahuacBiblioTestContext : DbContext
{
    public AoeganahuacBiblioTestContext()
    {
    }

    public AoeganahuacBiblioTestContext(DbContextOptions<AoeganahuacBiblioTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Medio> Medios { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TipoMedio> TipoMedios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-AD7LFKO5\\MSSQLSERVER1; Database=AOEGAnahuacBiblioTest; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

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
            entity.HasKey(e => e.IdMedio).HasName("PK__Medio__EF8018605FC49102");

            entity.ToTable("Medio");

            entity.Property(e => e.Publicacion).HasColumnType("date");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK__Medio__IdAutor__44FF419A");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK__Medio__IdEditori__4316F928");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__Medio__IdGenero__45F365D3");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK__Medio__IdIdioma__440B1D61");

            entity.HasOne(d => d.IdTipoMedioNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdTipoMedio)
                .HasConstraintName("FK__Medio__IdTipoMed__4222D4EF");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__Prestamo__6FF194C0AE07EFC8");

            entity.ToTable("Prestamo");

            entity.Property(e => e.FechaDevolucion).HasColumnType("date");
            entity.Property(e => e.FechaPrestamo).HasColumnType("date");
            entity.Property(e => e.Id).HasMaxLength(450);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__Prestamo__Id__04E4BC85");

            entity.HasOne(d => d.IdMedioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdMedio)
                .HasConstraintName("FK__Prestamo__IdMedi__05D8E0BE");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK__Prestamo__IdStat__06CD04F7");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__Status__B450643A091A5477");

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
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
