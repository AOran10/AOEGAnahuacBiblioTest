using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Idioma
    {
        public static ML.Result IdiomaAdd(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter nombre = new SqlParameter("@Nombre", idioma.Nombre);
                    string store = "IdiomaAdd @Nombre";
                    var query = context.Database.ExecuteSqlRaw(store, nombre);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado el idioma de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar el idioma";
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
        public static ML.Result IdiomaUpdate(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idIdioma = new SqlParameter("@IdIdioma", idioma.IdIdioma);
                    SqlParameter nombre = new SqlParameter("@Nombre", idioma.Nombre);
                    string store = "IdiomaUpdate @IdIdioma , @Nombre";
                    var query = context.Database.ExecuteSqlRaw(store, idIdioma, nombre);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado el idima de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo actualizar el idioma";
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
        public static ML.Result IdiomaDelete(int IdIdioma)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idIdioma = new SqlParameter("@IdIdioma", IdIdioma);

                    var query = context.Database.ExecuteSqlInterpolated($"IdiomaDelete {idIdioma}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado el idioma de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo eliminar el idioma";
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
        public static ML.Result IdiomaGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from idiomasLINQ in context.Idiomas
                                 select new
                                 {
                                     IdIdioma = idiomasLINQ.IdIdioma,
                                     Nombre = idiomasLINQ.Nombre
                                 }).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Idioma idioma = new ML.Idioma();
                                idioma.IdIdioma = item.IdIdioma;
                                idioma.Nombre = item.Nombre;

                                result.Objects.Add(idioma);
                            }
                            result.Correct = true;
                            result.Message = "Idiomas consultados";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "La tabla de idiomas esta vacia";
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
        public static ML.Result IdiomaGetById(int IdIdioma)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from idiomasLINQ in context.Idiomas
                                 where idiomasLINQ.IdIdioma == IdIdioma
                                 select new
                                 {
                                     IdIdioma = idiomasLINQ.IdIdioma,
                                     Nombre = idiomasLINQ.Nombre
                                 }
                                 ).FirstOrDefault();
                    if (query != null)
                    {
                        var item = query;
                        ML.Idioma idioma = new ML.Idioma();
                        idioma.IdIdioma = item.IdIdioma;
                        idioma.Nombre = item.Nombre;

                        result.Object = idioma;

                        result.Correct = true;
                        result.Message = "Idioma consultado";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar el idioma";
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
