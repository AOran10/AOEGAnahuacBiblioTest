using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Editorial
    {
        [Required]
        public int IdEditorial { get; set; }
        [DisplayName("Nombre de la editorial:")]
        [Required]
        [StringLength(50, ErrorMessage = "Solo se aceptan menos de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string? Nombre { get; set; }
        [DisplayName("Información de la editorial:")]
        [Required]
        [StringLength(120, ErrorMessage = "Solo se aceptan menos de 120 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "En este campo solo se aceptan Letras y numeros")]
        public string? InformacionAdicional { get; set; }
        [Required]
        [DisplayName("Imagen:")]
        public byte[]? Imagen { get; set; }
        public List<object>? Editoriales { get; set; }
    }
}
