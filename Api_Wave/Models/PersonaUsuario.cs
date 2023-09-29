using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class PersonaUsuario
{
    public int IdAlusuarui { get; set; }

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }

    public string IdPersona { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
