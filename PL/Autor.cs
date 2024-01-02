using System;
using System.Collections.Generic;

namespace PL;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string? Nombre { get; set; }

    public string? InformacionAdicional { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
