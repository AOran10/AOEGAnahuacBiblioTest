using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Status
    {
        public static ML.Result StatusGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from statusLINQ in context.Statuses
                                 select new
                                 {
                                     IdStatus = statusLINQ.IdStatus,
                                     Descripcion = statusLINQ.Descripcion
                                 }).ToList();

                    if(query != null)
                    {
                        if(query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Status status = new ML.Status();
                                status.IdStatus = item.IdStatus;
                                status.Descripcion = item.Descripcion;
                                result.Objects.Add(item);
                            }

                            result.Correct = true;
                            result.Message = "Mostrar Status";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "No Existen Valores";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "NO se pudo Completar la Consulta";
                    }
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
