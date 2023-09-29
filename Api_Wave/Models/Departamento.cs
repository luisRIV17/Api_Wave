using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Departamento
{
    public int IdDepa { get; set; }

    public string NombreDepa { get; set; } = null!;

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
