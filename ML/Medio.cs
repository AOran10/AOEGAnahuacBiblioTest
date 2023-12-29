using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        public int IdMedio { get; set; }
        public string Titulo { get; set; }
        public ML.TipoMedio TipoMedio { get; set; }
        public ML.Editorial Editorial { get; set; }
        public ML.Idioma Idioma { get; set; }
        public ML.Autor Autor { get; set; }
        public ML.Genero Genero { get; set; }
        public int Paginas { get; set; }
        public DateTime Publicacion { get; set; }
        public int CantidadEjemplares { get; set; }
        public int CantidadEnPrestamo { get; set; }
        public byte[] Imagen { get; set; }
    }
}
