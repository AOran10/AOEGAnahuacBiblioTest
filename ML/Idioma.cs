using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Idioma
    {
        [Required]
        public int IdIdioma { get; set; }
        [DisplayName("Nombre del idioma:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        [RegularExpression(@"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras")]
        public string? Nombre { get; set; }
        [Required]
        public List<object>? Idiomas { get; set; }
    }
}
