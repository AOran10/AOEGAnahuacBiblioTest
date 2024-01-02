using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Genero
    {
        [Required]
        public int IdGenero { get; set; }
        [DisplayName("Nombre del genero:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        [RegularExpression(@"^([a-zA-Z0-9áéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string? Nombre { get; set; }
        [DisplayName("Información del genero:")]
        [Required]
        [StringLength(120, ErrorMessage = "Solo se aceptan menos de 120 caracteres")]
        [RegularExpression(@"^([a-zA-Z0-9áéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string? Descripcion { get; set; }
        public List<object>? Generos { get; set; }
    }
}
