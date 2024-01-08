using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }

        public ML.IdentityUser IdentityUsers { get; set; }

        public ML.Medio Medio { get; set; }

        public DateTime? FechaPrestamo { get; set; }

        public DateTime? FechaDevolucion { get; set; }
        
        public ML.Status Status { get; set; }
        public List<object> Prestamos { get; set; }
             
    }
}
