using System;
using System.Collections.Generic;

namespace DL.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
