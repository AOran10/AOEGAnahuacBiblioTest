using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        [Required]
        public int IdMedio { get; set; }
        [DisplayName("Titulo:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        //[RegularExpression(@"^([a-zA-Z0-9áéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string Titulo { get; set; }
        public ML.TipoMedio TipoMedio { get; set; }
        public ML.Editorial Editorial { get; set; }
        public ML.Idioma Idioma { get; set; }
        public ML.Autor Autor { get; set; }
        public ML.Genero Genero { get; set; }
        public int Paginas { get; set; }
        public DateTime Publicacion { get; set; }
        public string PublicacionFormated { get; set; }
        public int CantidadEjemplares { get; set; }
        public int CantidadEnPrestamo { get; set; }
        [Required]
        [DisplayName("Imagen:")]
        public byte[] Imagen { get; set; }
        public ML.EstatusMedio EstatusMedio { get; set; }   
        public List<object> Medios { get; set; }
    }
}
