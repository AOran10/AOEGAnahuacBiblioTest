﻿using System;
using System.Collections.Generic;

namespace DL.Models;

public partial class Idioma
{
    public int IdIdioma { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
