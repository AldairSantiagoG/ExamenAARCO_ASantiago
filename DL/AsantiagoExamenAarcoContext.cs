using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class AsantiagoExamenAarcoContext : DbContext
{
    public AsantiagoExamenAarcoContext()
    {
    }

    public AsantiagoExamenAarcoContext(DbContextOptions<AsantiagoExamenAarcoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatalogoDescripcion> CatalogoDescripcions { get; set; }

    public virtual DbSet<Descripcion> Descripcions { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<ModeloSubMarca> ModeloSubMarcas { get; set; }

    public virtual DbSet<SubMarca> SubMarcas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G7UVJH5; Database= ASantiagoExamenAARCO; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogoDescripcion>(entity =>
        {
            entity.HasKey(e => e.IdCatalogoDescripcion).HasName("PK__Catalogo__02B4B611A710406B");

            entity.ToTable("CatalogoDescripcion");

            entity.Property(e => e.NombreDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Descripcion>(entity =>
        {
            entity.HasKey(e => e.IdDescripcion).HasName("PK__Descripc__130EFED6D7624673");

            entity.ToTable("Descripcion");

            entity.Property(e => e.IdDescripcion)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCatalogoDescripcionNavigation).WithMany(p => p.Descripcions)
                .HasForeignKey(d => d.IdCatalogoDescripcion)
                .HasConstraintName("FK__Descripci__IdCat__1A14E395");

            entity.HasOne(d => d.IdModeloSubMarcaNavigation).WithMany(p => p.Descripcions)
                .HasForeignKey(d => d.IdModeloSubMarca)
                .HasConstraintName("FK__Descripci__IdMod__1920BF5C");

            entity.HasOne(d => d.IdSubMarcaNavigation).WithMany(p => p.Descripcions)
                .HasForeignKey(d => d.IdSubMarca)
                .HasConstraintName("FK__Descripci__IdSub__1B0907CE");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A8879AA6B967");

            entity.ToTable("Marca");

            entity.Property(e => e.NombreMarca)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ModeloSubMarca>(entity =>
        {
            entity.HasKey(e => e.IdModeloSubMarca).HasName("PK__ModeloSu__4544C2D492D13596");

            entity.ToTable("ModeloSubMarca");

            entity.Property(e => e.IdModeloSubMarca).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<SubMarca>(entity =>
        {
            entity.HasKey(e => e.IdSubMarca).HasName("PK__SubMarca__40FA6C1940744DFA");

            entity.ToTable("SubMarca");

            entity.Property(e => e.NombreSubMarca)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.SubMarcas)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__SubMarca__IdMarc__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
