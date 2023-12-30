using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Autor
    {
        [Required]
        public int IdAutor { get; set; }
        [DisplayName("Nombre del autor:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        [RegularExpression(@"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras")]
        public string? Nombre { get; set; }
        [DisplayName("Informacion del autor:")]
        [Required]
        [StringLength(120, ErrorMessage = "Solo se aceptan menos de 120 caracteres")]
        [RegularExpression(@"^([a-zA-Z0-9áéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string? InformacionAdicional { get; set; }
        [Required]
        [DisplayName("Imagen:")]
        public byte[]? Imagen { get; set; }
        public List<object>? Autores { get; set; }
    }
}
