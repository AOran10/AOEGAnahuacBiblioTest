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

        public ML.Medio IdMedio { get; set; }

        public DateTime? FechaPrestamo { get; set; }

        public DateTime? FechaDevolucion { get; set; }
        
        public ML.Status IdStatus { get; set; }
        public List<object> Prestamos { get; set; }
             
    }
}
