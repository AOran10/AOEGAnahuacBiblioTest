using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusPrestamo
{
    public int IdEstatusPrestamo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
