using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Editorial
    {
        public static ML.Result EditorialAdd(ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter nombre = new SqlParameter("@Nombre", editorial.Nombre);
                    SqlParameter informacionAdicional = new SqlParameter("@InformacionAdicional", editorial.InformacionAdicional);
                    SqlParameter imagen = new SqlParameter("@Imagen", editorial.Imagen);
                    string store = "EditorialAdd @Nombre , @InformacionAdicional , @Imagen";
                    var query = context.Database.ExecuteSqlRaw(store, nombre, informacionAdicional, imagen);
                    //var query = context.Database.ExecuteSqlInterpolated($"AutorAdd {nombre}, {informacionAdicional}, {imagen}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado la editorial de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar la editorial";
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
        public static ML.Result EditorialUpdate(ML.Editorial editorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idEditorial = new SqlParameter("@IdEditorial", editorial.IdEditorial);
                    SqlParameter nombre = new SqlParameter("@Nombre", editorial.Nombre);
                    SqlParameter informacionAdicional = new SqlParameter("@InformacionAdicional", editorial.InformacionAdicional);
                    SqlParameter imagen = new SqlParameter("@Imagen", editorial.Imagen);
                    string store = "EditorialUpdate @IdEditorial , @Nombre , @InformacionAdicional , @Imagen";
                    var query = context.Database.ExecuteSqlRaw(store, idEditorial, nombre, informacionAdicional, imagen);
                    //var query = context.Database.ExecuteSqlInterpolated($"AutorAdd {nombre}, {informacionAdicional}, {imagen}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado la editorial de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo actializar la editorial";
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
        public static ML.Result EditorialDelete(int IdEditorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idEditorial = new SqlParameter("@IdEditorial", IdEditorial);
                    //string store = "EditorialDelete @IdEditorial";
                    //var query = context.Database.ExecuteSqlRaw(store, idEditorial);
                    var query = context.Database.ExecuteSqlInterpolated($"EditorialDelete {idEditorial}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado la editorial de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo eliminar la editorial";
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
        public static ML.Result EditorialGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from editorialesLINQ in context.Editorials
                                 select new
                                 {
                                     IdEditorial = editorialesLINQ.IdEditorial,
                                     Nombre = editorialesLINQ.Nombre,
                                     InformacionAdicional = editorialesLINQ.InformacionAdicional,
                                     Imagen = editorialesLINQ.Imagen
                                 }).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Editorial editorial = new ML.Editorial();
                                editorial.IdEditorial = item.IdEditorial;
                                editorial.Nombre = item.Nombre;
                                editorial.InformacionAdicional = item.InformacionAdicional;
                                editorial.Imagen = item.Imagen;

                                result.Objects.Add(editorial);
                            }
                            result.Correct = true;
                            result.Message = "Editoriales consultados";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "La tabla de editoriales esta vacia";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar las editoriales";
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
        public static ML.Result EditorialGetById(int IdEditorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from editorialesLINQ in context.Editorials
                                 where editorialesLINQ.IdEditorial == IdEditorial
                                 select new
                                 {
                                     IdEditorial = editorialesLINQ.IdEditorial,
                                     Nombre = editorialesLINQ.Nombre,
                                     InformacionAdicional = editorialesLINQ.InformacionAdicional,
                                     Imagen = editorialesLINQ.Imagen
                                 }
                                 ).FirstOrDefault();
                    if (query != null)
                    {
                        var item = query;
                        ML.Editorial editorial = new ML.Editorial();
                        editorial.IdEditorial = item.IdEditorial;
                        editorial.Nombre = item.Nombre;
                        editorial.InformacionAdicional = item.InformacionAdicional;
                        editorial.Imagen = item.Imagen;

                        result.Object = editorial;

                        result.Correct = true;
                        result.Message = "Editorial consultado";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar la editorial";
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
