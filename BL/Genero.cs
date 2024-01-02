using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Genero
    {
        public static ML.Result GeneroAdd(ML.Genero genero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter nombre = new SqlParameter("@Nombre", genero.Nombre);
                    SqlParameter descripcion = new SqlParameter("@Descripcion", genero.Descripcion);
                    string store = "GeneroAdd @Nombre , @Descripcion";
                    var query = context.Database.ExecuteSqlRaw(store, nombre, descripcion);
                    //var query = context.Database.ExecuteSqlInterpolated($"AutorAdd {nombre}, {informacionAdicional}, {imagen}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado el genero de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar el genero";
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
        public static ML.Result GeneroUpdate(ML.Genero genero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idGenero = new SqlParameter("@IdGenero", genero.IdGenero);
                    SqlParameter nombre = new SqlParameter("@Nombre", genero.Nombre);
                    SqlParameter descripcion = new SqlParameter("@Descripcion", genero.Descripcion);
                    string store = "GeneroUpdate @IdGenero , @Nombre , @Descripcion";
                    var query = context.Database.ExecuteSqlRaw(store, idGenero, nombre, descripcion);
                    //var query = context.Database.ExecuteSqlInterpolated($"AutorAdd {nombre}, {informacionAdicional}, {imagen}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado el genero de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo actualizar el genero";
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
        public static ML.Result GeneroDelete(int IdGenero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idGenero = new SqlParameter("@IdGenero", IdGenero);
                    //string store = "EditorialDelete @IdEditorial";
                    //var query = context.Database.ExecuteSqlRaw(store, idEditorial);
                    var query = context.Database.ExecuteSqlInterpolated($"GeneroDelete {idGenero}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado el genero de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo eliminar el genero";
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
        public static ML.Result GeneroGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from generosLINQ in context.Generos
                                 select new
                                 {
                                     IdGenero = generosLINQ.IdGenero,
                                     Nombre = generosLINQ.Nombre,
                                     Descripcion = generosLINQ.Descripcion
                                 }).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Genero genero = new ML.Genero();
                                genero.IdGenero = item.IdGenero;
                                genero.Nombre = item.Nombre;
                                genero.Descripcion = item.Descripcion;

                                result.Objects.Add(genero);
                            }
                            result.Correct = true;
                            result.Message = "Generos consultados";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "La tabla de generos esta vacia";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar los generos";
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
        public static ML.Result GeneroGetById(int IdGenero)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from generosLINQ in context.Generos
                                 where generosLINQ.IdGenero == IdGenero
                                 select new
                                 {
                                     IdGenero = generosLINQ.IdGenero,
                                     Nombre = generosLINQ.Nombre,
                                     Descripcion = generosLINQ.Descripcion
                                 }
                                 ).FirstOrDefault();
                    if (query != null)
                    {
                        var item = query;
                        ML.Genero genero = new ML.Genero();
                        genero.IdGenero = item.IdGenero;
                        genero.Nombre = item.Nombre;
                        genero.Descripcion = item.Descripcion;

                        result.Object = genero;

                        result.Correct = true;
                        result.Message = "Genero consultado";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar el genero";
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
