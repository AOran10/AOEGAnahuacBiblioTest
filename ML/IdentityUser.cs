﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class IdentityUser
    {
        public string IdUsuario { get; set; }
        public string UserName { get; set; }

        // PRUEBA
        public string Password { get; set; }  
        public string Email { get; set; }    

        //
        public ML.Rol Rol { get; set; }
        public List<object> IdentityUsers { get; set; }
    }
}
