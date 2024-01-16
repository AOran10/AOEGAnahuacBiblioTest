using System;
using System.Collections.Generic;

namespace DL;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public string? IdUsuario { get; set; }

    public int? IdMedio { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public int? IdEstatus { get; set; }

    public virtual EstatusPrestamo? IdEstatusNavigation { get; set; }

    public virtual Medio? IdMedioNavigation { get; set; }

    public virtual AspNetUser? IdUsuarioNavigation { get; set; }
}
