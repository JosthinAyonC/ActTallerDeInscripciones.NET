using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ActividadDeTalleres.Models;

public partial class ActTallerContext : DbContext
{
    public ActTallerContext()
    {
    }

    public ActTallerContext(DbContextOptions<ActTallerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<Tallere> Talleres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=actTaller;User Id=postgres;Password=lokoloko21;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("asistencias_pkey");

            entity.ToTable("asistencias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaAsistencia)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asistencia");
            entity.Property(e => e.ParticipanteId).HasColumnName("participante_id");
            entity.Property(e => e.TallerId).HasColumnName("taller_id");

            entity.HasOne(d => d.Participante).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("asistencias_participante_id_fkey");

            entity.HasOne(d => d.Taller).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.TallerId)
                .HasConstraintName("asistencias_taller_id_fkey");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inscripciones_pkey");

            entity.ToTable("inscripciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inscripcion");
            entity.Property(e => e.ParticipanteId).HasColumnName("participante_id");
            entity.Property(e => e.TallerId).HasColumnName("taller_id");

            entity.HasOne(d => d.Participante).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("inscripciones_participante_id_fkey");

            entity.HasOne(d => d.Taller).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.TallerId)
                .HasConstraintName("inscripciones_taller_id_fkey");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participantes_pkey");

            entity.ToTable("participantes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Tallere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("talleres_pkey");

            entity.ToTable("talleres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CupoMaximo).HasColumnName("cupo_maximo");
            entity.Property(e => e.DuracionHoras).HasColumnName("duracion_horas");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
