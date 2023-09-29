using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class TipoSala
{
    public int IdTipoSala { get; set; }

    public string NombreTipoSala { get; set; } = null!;

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();
}
