using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static ML.Result AutorAdd(ML.Autor autor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
