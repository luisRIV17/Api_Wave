using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Contacto
{
    public int IdContacto { get; set; }

    public int UsuarioContacto { get; set; }

    public bool EstadoContacto { get; set; }

    public string AliasContacto { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
