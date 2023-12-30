using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class TipoMedio
    {
        [Required]
        public int IdTipoMedio { get; set; }
        [DisplayName("Nombre del tipo de medio:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        [RegularExpression(@"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]{2,60}[\,\-\.]{0,1}[\s]{0,1}){1,3}$", ErrorMessage = "En este campo solo se aceptan Letras")]
        public string? Nombre { get; set; }
        [DisplayName("Descripción del tipo de medio:")]
        [Required]
        [StringLength(120, ErrorMessage = "Solo se aceptan menos de 120 caracteres")]
        public string? Descripcion { get; set; }
        public List<object>? TipoMedios { get; set; }
    }
}
