using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoMedio
    {
        public static ML.Result TipoMedioAdd(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter nombre = new SqlParameter("@Nombre", tipoMedio.Nombre);
                    SqlParameter descripcion = new SqlParameter("@Descripcion", tipoMedio.Descripcion);
                    string store = "TipoMedioAdd @Nombre , @Descripcion";
                    var query = context.Database.ExecuteSqlRaw(store, nombre, descripcion);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado el tipo de medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar el tipo de medio";
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
        public static ML.Result TipoMedioUpdate(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idTipoMedio = new SqlParameter("@IdTipoMedio", tipoMedio.IdTipoMedio);
                    SqlParameter nombre = new SqlParameter("@Nombre", tipoMedio.Nombre);
                    SqlParameter descripcion = new SqlParameter("@Descripcion", tipoMedio.Descripcion);
                    string store = "TipoMedioUpdate @IdTipoMedio , @Nombre , @Descripcion";
                    var query = context.Database.ExecuteSqlRaw(store, idTipoMedio, nombre, descripcion);
                    //var query = context.Database.ExecuteSqlInterpolated($"AutorAdd {nombre}, {informacionAdicional}, {imagen}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado el tipo de medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo actualizar el tipo de medio";
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
        public static ML.Result TipoMedioDelete(int IdTipoMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idTipoMedio = new SqlParameter("@IdTipoMedio", IdTipoMedio); 
                    var query = context.Database.ExecuteSqlInterpolated($"TipoMedioDelete {idTipoMedio}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado el tipo de medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo eliminar el tipo de medio";
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
        public static ML.Result TipoMedioGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from tipoMediosLINQ in context.TipoMedios
                                 select new
                                 {
                                     IdTipoMedio = tipoMediosLINQ.IdTipoMedio,
                                     Nombre = tipoMediosLINQ.Nombre,
                                     Descripcion = tipoMediosLINQ.Descripcion
                                 }).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.TipoMedio tipoMedio = new ML.TipoMedio();
                                tipoMedio.IdTipoMedio = item.IdTipoMedio;
                                tipoMedio.Nombre = item.Nombre;
                                tipoMedio.Descripcion = item.Descripcion;

                                result.Objects.Add(tipoMedio);
                            }
                            result.Correct = true;
                            result.Message = "Tipos de medio consultados";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "La tabla de Tipos de medio esta vacia";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar los tipos de medio";
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
        public static ML.Result TipoMedioGetById(int IdTipoMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from tipoMediosLINQ in context.TipoMedios
                                 where tipoMediosLINQ.IdTipoMedio == IdTipoMedio
                                 select new
                                 {
                                     IdTipoMedio = tipoMediosLINQ.IdTipoMedio,
                                     Nombre = tipoMediosLINQ.Nombre,
                                     Descripcion = tipoMediosLINQ.Descripcion
                                 }
                                 ).FirstOrDefault();
                    if (query != null)
                    {
                        var item = query;
                        ML.TipoMedio tipoMedio = new ML.TipoMedio();
                        tipoMedio.IdTipoMedio = item.IdTipoMedio;
                        tipoMedio.Nombre = item.Nombre;
                        tipoMedio.Descripcion = item.Descripcion;

                        result.Object = tipoMedio;

                        result.Correct = true;
                        result.Message = "Tipo de medio consultado";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar el tipo de medio";
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
