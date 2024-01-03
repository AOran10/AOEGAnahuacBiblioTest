using System;
using System.Collections.Generic;

namespace DL;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public string? Id { get; set; }

    public int? IdMedio { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public int? IdStatus { get; set; }

    public virtual Medio? IdMedioNavigation { get; set; }

    public virtual AspNetUser? IdNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }
}
