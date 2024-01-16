using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstatusPrestamo
    {
        public int IdEstatusPrestamo { get; set; }
        public string? Descripcion { get; set; }
        public List<object> EstatusPrestamoList { get; set; }
    }
}