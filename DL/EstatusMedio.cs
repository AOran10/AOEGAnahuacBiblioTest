using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusMedio
{
    public int IdEstatusMedio { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
