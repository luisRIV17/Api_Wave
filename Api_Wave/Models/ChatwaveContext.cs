using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api_Wave.Models;

public partial class ChatwaveContext : DbContext
{
    public ChatwaveContext()
    {
    }

    public ChatwaveContext(DbContextOptions<ChatwaveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<DetalleEstadoMensaje> DetalleEstadoMensajes { get; set; }

    public virtual DbSet<EstadoMensaje> EstadoMensajes { get; set; }

    public virtual DbSet<EstadoSala> EstadoSalas { get; set; }

    public virtual DbSet<IntegrantesSala> IntegrantesSalas { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonaUsuario> PersonaUsuarios { get; set; }

    public virtual DbSet<RegistroLlamadum> RegistroLlamada { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<TipoLectura> TipoLecturas { get; set; }

    public virtual DbSet<TipoSala> TipoSalas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasKey(e => e.IdContacto);

            entity.ToTable("Contacto");

            entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
            entity.Property(e => e.AliasContacto)
                .HasMaxLength(30)
                .HasColumnName("alias_contacto");
            entity.Property(e => e.EstadoContacto).HasColumnName("estado_contacto");
            entity.Property(e => e.Fecha)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.UsuarioContacto)
                .HasMaxLength(50)
                .HasColumnName("usuario_contacto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Contactos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contacto_Usuario");
        });

        modelBuilder.Entity<DetalleEstadoMensaje>(entity =>
        {
            entity.HasKey(e => e.IdDetalleEstado);

            entity.ToTable("Detalle_estado_mensaje");

            entity.Property(e => e.IdDetalleEstado).HasColumnName("id_detalle_estado");
            entity.Property(e => e.FechaEstadoDet)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha_estado_det");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");
            entity.Property(e => e.IdTipoLectura).HasColumnName("id_tipo_lectura");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.DetalleEstadoMensajes)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Detalle_estado_mensaje_Estado_mensaje");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.DetalleEstadoMensajes)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_estado_mensaje_Integrantes_sala");

            entity.HasOne(d => d.IdTipoLecturaNavigation).WithMany(p => p.DetalleEstadoMensajes)
                .HasForeignKey(d => d.IdTipoLectura)
                .HasConstraintName("FK_Detalle_estado_mensaje_Tipo_lectura");
        });

        modelBuilder.Entity<EstadoMensaje>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("Estado_mensaje");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdMensaje).HasColumnName("id_mensaje");
            entity.Property(e => e.IdTipoSala).HasColumnName("id_tipo_sala");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(35)
                .HasColumnName("nombre_estado");

            entity.HasOne(d => d.IdMensajeNavigation).WithMany(p => p.EstadoMensajes)
                .HasForeignKey(d => d.IdMensaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estado_mensaje_Mensajes");
        });

        modelBuilder.Entity<EstadoSala>(entity =>
        {
            entity.HasKey(e => e.IdEstadosala);

            entity.ToTable("Estado_sala");

            entity.Property(e => e.IdEstadosala)
                .ValueGeneratedNever()
                .HasColumnName("id_estadosala");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.EstadoSalas)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estado_sala_Integrantes_sala");
        });

        modelBuilder.Entity<IntegrantesSala>(entity =>
        {
            entity.HasKey(e => e.IdIntegrante);

            entity.ToTable("Integrantes_sala");

            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");
            entity.Property(e => e.EstadoAdministrador).HasColumnName("estado_administrador");
            entity.Property(e => e.EstadoIntegrante).HasColumnName("estado_integrante");
            entity.Property(e => e.IdPersona)
                .HasMaxLength(25)
                .HasColumnName("id_persona");
            entity.Property(e => e.IdSala)
                .HasMaxLength(25)
                .HasColumnName("id_sala");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.IntegrantesSalas)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Integrantes_sala_Persona");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.IntegrantesSalas)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Integrantes_sala_Sala");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.IdMensaje);

            entity.Property(e => e.IdMensaje).HasColumnName("id_mensaje");
            entity.Property(e => e.Archivo)
                .HasMaxLength(200)
                .HasColumnName("archivo");
            entity.Property(e => e.Audio)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("audio");
            entity.Property(e => e.EstadoMensaje).HasColumnName("estado_mensaje");
            entity.Property(e => e.FechaMensaje)
                .HasColumnType("datetime")
                .HasColumnName("fecha_mensaje");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");
            entity.Property(e => e.IdSala)
                .HasMaxLength(25)
                .HasColumnName("id_sala");
            entity.Property(e => e.Imagen)
                .HasColumnType("image")
                .HasColumnName("imagen");
            entity.Property(e => e.Mensaje1)
                .HasColumnType("text")
                .HasColumnName("mensaje");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensajes_Integrantes_sala");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.IdSala)
                .HasConstraintName("FK_Mensajes_Sala");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("Persona");

            entity.Property(e => e.IdPersona)
                .HasMaxLength(25)
                .HasColumnName("id_persona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(25)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(25)
                .HasColumnName("correo");
            entity.Property(e => e.FechaNac)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha_nac");
            entity.Property(e => e.FotoPerfil)
                .HasColumnType("image")
                .HasColumnName("foto_perfil");
            entity.Property(e => e.Leyenda)
                .HasMaxLength(100)
                .HasColumnName("leyenda");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<PersonaUsuario>(entity =>
        {
            entity.HasKey(e => e.IdAlusuarui);

            entity.ToTable("Persona_usuario");

            entity.Property(e => e.IdAlusuarui).HasColumnName("id_alusuarui");
            entity.Property(e => e.Fecha)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPersona)
                .HasMaxLength(25)
                .HasColumnName("id_persona");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.PersonaUsuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_usuario_Persona");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PersonaUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_usuario_Usuario");
        });

        modelBuilder.Entity<RegistroLlamadum>(entity =>
        {
            entity.HasKey(e => e.IdRegistroLlamada);

            entity.ToTable("Registro_Llamada");

            entity.Property(e => e.IdRegistroLlamada)
                .ValueGeneratedNever()
                .HasColumnName("id_registro_llamada");
            entity.Property(e => e.Duracion)
                .HasMaxLength(8)
                .HasColumnName("duracion");
            entity.Property(e => e.FechaLlamada)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha_llamada");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.RegistroLlamada)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Llamada_Integrantes_sala");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala);

            entity.ToTable("Sala");

            entity.Property(e => e.IdSala)
                .HasMaxLength(25)
                .HasColumnName("id_sala");
            entity.Property(e => e.EstadoChat).HasColumnName("estado_chat");
            entity.Property(e => e.FechaIncio)
                .HasColumnType("smalldatetime")
                .HasColumnName("fecha_incio");
            entity.Property(e => e.IdTipoSala).HasColumnName("id_tipo_sala");
            entity.Property(e => e.NombreSala)
                .HasMaxLength(50)
                .HasColumnName("nombre_sala");

            entity.HasOne(d => d.IdTipoSalaNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.IdTipoSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sala_Tipo_sala");
        });

        modelBuilder.Entity<TipoLectura>(entity =>
        {
            entity.HasKey(e => e.IdTipoLectura);

            entity.ToTable("Tipo_lectura");

            entity.Property(e => e.IdTipoLectura).HasColumnName("id_tipo_lectura");
            entity.Property(e => e.NombreTipoLectura)
                .HasMaxLength(50)
                .HasColumnName("nombre_tipo_lectura");
        });

        modelBuilder.Entity<TipoSala>(entity =>
        {
            entity.HasKey(e => e.IdTipoSala);

            entity.ToTable("Tipo_sala");

            entity.Property(e => e.IdTipoSala)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_sala");
            entity.Property(e => e.NombreTipoSala)
                .HasMaxLength(50)
                .HasColumnName("nombre_tipo_sala");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(25)
                .HasColumnName("contraseña");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
