using System;
using System.Collections.Generic;

namespace DL;

public partial class Editorial
{
    public int IdEditorial { get; set; }

    public string? Nombre { get; set; }

    public string? InformacionAdicional { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
