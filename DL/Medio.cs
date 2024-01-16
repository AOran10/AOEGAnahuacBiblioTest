using System;
using System.Collections.Generic;

namespace DL;

public partial class Medio
{
    public int IdMedio { get; set; }

    public string? Titulo { get; set; }

    public int? IdTipoMedio { get; set; }

    public int? IdEditorial { get; set; }

    public int? IdIdioma { get; set; }

    public int? IdAutor { get; set; }

    public int? IdGenero { get; set; }

    public int? Paginas { get; set; }

    public DateTime? Publicacion { get; set; }

    public int? CantidadEjemplares { get; set; }

    public int? CantidadEnPrestamo { get; set; }

    public byte[]? Imagen { get; set; }

    public int? IdEstatus { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }

    public virtual EstatusMedio? IdEstatusNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual TipoMedio? IdTipoMedioNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
