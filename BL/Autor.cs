using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                    SqlParameter nombre = new SqlParameter("@Nombre", autor.Nombre);
                    SqlParameter informacionAdicional = new SqlParameter("@InformacionAdicional", autor.InformacionAdicional);
                    SqlParameter imagen = new SqlParameter("@Imagen", autor.Imagen);
                    string store = "AutorAdd = @Nombre @InformacionAdicional @Imagen";
                    var query = context.Database.ExecuteSqlRaw(store, nombre, informacionAdicional, imagen);
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado al autor de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar al autor";
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
        public static ML.Result AutorGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from autoresLINQ in context.Autors
                                 select new
                                 {
                                     IdAutor = autoresLINQ.IdAutor,
                                     Nombre = autoresLINQ.Nombre,
                                     InformacionAdicional = autoresLINQ.InformacionAdicional,
                                     Imagen = autoresLINQ.Imagen
                                 }).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Autor autor = new ML.Autor();
                            autor.IdAutor = item.IdAutor;
                            autor.Nombre = item.Nombre;
                            autor.InformacionAdicional = item.InformacionAdicional;
                            autor.Imagen = item.Imagen;

                            result.Objects.Add(autor);
                        }
                        result.Correct = true;
                        result.Message = "Autores consultados";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar los autores";
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
