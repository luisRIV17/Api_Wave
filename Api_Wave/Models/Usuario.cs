using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();

    public virtual ICollection<PersonaUsuario> PersonaUsuarios { get; set; } = new List<PersonaUsuario>();
}
