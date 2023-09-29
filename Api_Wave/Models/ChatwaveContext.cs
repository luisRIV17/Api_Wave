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

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<EstadoMensaje> EstadoMensajes { get; set; }

    public virtual DbSet<EstadoSala> EstadoSalas { get; set; }

    public virtual DbSet<IntegrantesSala> IntegrantesSalas { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonaUsuario> PersonaUsuarios { get; set; }

    public virtual DbSet<RegistroLlamadum> RegistroLlamada { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<TipoSala> TipoSalas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=chatwave;Persist Security Info=True;User ID=sa;Password=luis17111989;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasKey(e => e.IdContacto).HasName("PK__Contacto__099A52B823F731FB");

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
            entity.Property(e => e.UsuarioContacto).HasColumnName("usuario_contacto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Contactos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacto__id_usu__4E88ABD4");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepa).HasName("PK__Departam__900B8F8417088E48");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepa)
                .ValueGeneratedNever()
                .HasColumnName("id_depa");
            entity.Property(e => e.NombreDepa)
                .HasMaxLength(35)
                .HasColumnName("nombre_depa");
        });

        modelBuilder.Entity<EstadoMensaje>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado_m__86989FB22357A56D");

            entity.ToTable("Estado_mensaje");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");
            entity.Property(e => e.IdMensaje).HasColumnName("id_mensaje");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(35)
                .HasColumnName("nombre_estado");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.EstadoMensajes)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estado_me__id_in__59063A47");

            entity.HasOne(d => d.IdMensajeNavigation).WithMany(p => p.EstadoMensajes)
                .HasForeignKey(d => d.IdMensaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estado_me__id_me__534D60F1");
        });

        modelBuilder.Entity<EstadoSala>(entity =>
        {
            entity.HasKey(e => e.IdEstadosala).HasName("PK__Estado_s__26D2B09763AF3911");

            entity.ToTable("Estado_sala");

            entity.Property(e => e.IdEstadosala)
                .ValueGeneratedNever()
                .HasColumnName("id_estadosala");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdIntegrante).HasColumnName("id_integrante");

            entity.HasOne(d => d.IdIntegranteNavigation).WithMany(p => p.EstadoSalas)
                .HasForeignKey(d => d.IdIntegrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estado_sa__id_in__5812160E");
        });

        modelBuilder.Entity<IntegrantesSala>(entity =>
        {
            entity.HasKey(e => e.IdIntegrante).HasName("PK__Integran__3A0EF5AB312C182D");

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
                .HasConstraintName("FK__Integrant__id_pe__5165187F");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.IntegrantesSalas)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Integrant__id_sa__52593CB8");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.IdMensaje).HasName("PK__Mensajes__5B37C7F6954F33EA");

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
                .HasColumnType("smalldatetime")
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
                .HasConstraintName("FK__Mensajes__id_int__5629CD9C");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.IdSala)
                .HasConstraintName("FK_Mensajes_Sala");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__01C9EB99F63A0DCF");

            entity.ToTable("Municipio");

            entity.Property(e => e.IdMunicipio)
                .ValueGeneratedNever()
                .HasColumnName("id_municipio");
            entity.Property(e => e.IdDepa).HasColumnName("id_depa");
            entity.Property(e => e.NombreMunicipio)
                .HasMaxLength(35)
                .HasColumnName("nombre_municipio");

            entity.HasOne(d => d.IdDepaNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdDepa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__id_de__5535A963");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__228148B00ADA10B7");

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
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.Leyenda)
                .HasMaxLength(100)
                .HasColumnName("leyenda");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Persona__id_muni__5441852A");
        });

        modelBuilder.Entity<PersonaUsuario>(entity =>
        {
            entity.HasKey(e => e.IdAlusuarui).HasName("PK__Alumno_U__530248E6B5C6272C");

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
                .HasConstraintName("FK__Alumno_Us__id_pe__5070F446");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PersonaUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alumno_Us__id_us__4F7CD00D");
        });

        modelBuilder.Entity<RegistroLlamadum>(entity =>
        {
            entity.HasKey(e => e.IdRegistroLlamada).HasName("PK__Registro__6FD3008D03ECD594");

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
                .HasConstraintName("FK__Registro___id_in__571DF1D5");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PK__Sala__D18B015BACA2A2A4");

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
                .HasConstraintName("FK__Sala__id_tipo_sa__59FA5E80");
        });

        modelBuilder.Entity<TipoSala>(entity =>
        {
            entity.HasKey(e => e.IdTipoSala).HasName("PK__Tipo_sal__1C51F013241A7EBF");

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
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD6BC8A6DC");

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
