using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Municipio
{
    public int IdMunicipio { get; set; }

    public string NombreMunicipio { get; set; } = null!;

    public int IdDepa { get; set; }

    public virtual Departamento IdDepaNavigation { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
