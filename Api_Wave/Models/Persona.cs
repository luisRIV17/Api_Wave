using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Persona
{
    public string IdPersona { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime? FechaNac { get; set; }

    public byte[]? FotoPerfil { get; set; }

    public string Leyenda { get; set; } = null!;

    public virtual ICollection<IntegrantesSala> IntegrantesSalas { get; set; } = new List<IntegrantesSala>();

    public virtual ICollection<PersonaUsuario> PersonaUsuarios { get; set; } = new List<PersonaUsuario>();
}
