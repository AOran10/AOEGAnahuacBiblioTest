using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medio
    {
        public static ML.Result MedioAdd(ML.Medio medio)
        {
            ML.Result result = new ML.Result();
            medio.EstatusMedio = new ML.EstatusMedio();
            medio.EstatusMedio.IdEstatusMedio = 1;

            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter titulo = new SqlParameter("@Titulo", medio.Titulo);
                    SqlParameter idTipoMedio = new SqlParameter("@IdTipoMedio", medio.TipoMedio.IdTipoMedio);
                    SqlParameter idEditorial = new SqlParameter("@IdEditorial", medio.Editorial.IdEditorial);
                    SqlParameter idIdioma = new SqlParameter("@IdIdioma", medio.Idioma.IdIdioma);
                    SqlParameter idAutor = new SqlParameter("@IdAutor", medio.Autor.IdAutor);
                    SqlParameter idGenero = new SqlParameter("@IdGenero", medio.Genero.IdGenero);
                    SqlParameter paginas = new SqlParameter("@Paginas", medio.Paginas);
                    SqlParameter publicacion = new SqlParameter("@Publicacion", medio.Publicacion);
                    SqlParameter cantidadEjemplares = new SqlParameter("@CantidadEjemplares", medio.CantidadEjemplares);
                    SqlParameter imagen = new SqlParameter("@Imagen", medio.Imagen);
                    SqlParameter idEstatusMedio = new SqlParameter("@IdEstatusMedio", medio.EstatusMedio.IdEstatusMedio);


                    string store = "MedioAdd @Titulo , @IdTipoMedio , @IdEditorial , @IdIdioma , @IdAutor , @IdGenero , @Paginas , @Publicacion , @CantidadEjemplares , @Imagen, @IdEstatusMedio ";
                    var query = context.Database.ExecuteSqlRaw(store, titulo, idTipoMedio, idEditorial, idIdioma, idAutor, idGenero, paginas, publicacion, cantidadEjemplares, imagen, idEstatusMedio);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha ingresado el medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo ingresar el medio";
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
        public static ML.Result MedioUpdate(ML.Medio medio)
        {
            ML.Result result = new ML.Result();
            medio.EstatusMedio = new ML.EstatusMedio();
            medio.EstatusMedio.IdEstatusMedio = 1;
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idMedio = new SqlParameter("@IdMedio", medio.IdMedio);
                    SqlParameter titulo = new SqlParameter("@Titulo", medio.Titulo);
                    SqlParameter idTipoMedio = new SqlParameter("@IdTipoMedio", medio.TipoMedio.IdTipoMedio);
                    SqlParameter idEditorial = new SqlParameter("@IdEditorial", medio.Editorial.IdEditorial);
                    SqlParameter idIdioma = new SqlParameter("@IdIdioma", medio.Idioma.IdIdioma);
                    SqlParameter idAutor = new SqlParameter("@IdAutor", medio.Autor.IdAutor);
                    SqlParameter idGenero = new SqlParameter("@IdGenero", medio.Genero.IdGenero);
                    SqlParameter paginas = new SqlParameter("@Paginas", medio.Paginas);
                    SqlParameter publicacion = new SqlParameter("@Publicacion", medio.Publicacion);
                    SqlParameter cantidadEjemplares = new SqlParameter("@CantidadEjemplares", medio.CantidadEjemplares);
                    SqlParameter cantidadEnPrestamo = new SqlParameter("@CantidadEnPrestamo", medio.CantidadEnPrestamo);
                    SqlParameter imagen = new SqlParameter("@Imagen", medio.Imagen);
                    SqlParameter idEstatusMedio = new SqlParameter("@IdEstatusMedio", medio.EstatusMedio.IdEstatusMedio);
                    string store = "MedioUpdate @IdMedio , @Titulo , @IdTipoMedio , @IdEditorial , @IdIdioma , @IdAutor , @IdGenero , @Paginas , @Publicacion , @CantidadEjemplares , @CantidadEnPrestamo , @Imagen, @IdEstatusMedio";
                    var query = context.Database.ExecuteSqlRaw(store, idMedio, titulo, idTipoMedio, idEditorial, idIdioma, idAutor, idGenero, paginas, publicacion, cantidadEjemplares, cantidadEnPrestamo, imagen , idEstatusMedio);


                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado el medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo actualizar el medio";
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
        public static ML.Result MedioDelete(int IdMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idMedio = new SqlParameter("@IdMedio", IdMedio);
                    var query = context.Database.ExecuteSqlInterpolated($"MedioDelete {idMedio}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado el medio de manera correcta";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo eliminar el medio";
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
        public static ML.Result MedioGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from mediosLINQ in context.Medios
                                 join tipoMedioLINQ in context.TipoMedios on mediosLINQ.IdTipoMedio equals tipoMedioLINQ.IdTipoMedio
                                 join editorialLINQ in context.Editorials on mediosLINQ.IdEditorial equals editorialLINQ.IdEditorial
                                 join idiomaLINQ in context.Idiomas on mediosLINQ.IdIdioma equals idiomaLINQ.IdIdioma
                                 join autorLINQ in context.Autors on mediosLINQ.IdAutor equals autorLINQ.IdAutor
                                 join generoLINQ in context.Generos on mediosLINQ.IdGenero equals generoLINQ.IdGenero
                                 select new
                                 {
                                     IdMedio = mediosLINQ.IdMedio,
                                     Titulo = mediosLINQ.Titulo,
                                     IdTipoMedio = mediosLINQ.IdTipoMedio,
                                     NombreTipoMedio = tipoMedioLINQ.Nombre,
                                     IdEditorial = mediosLINQ.IdEditorial,
                                     NombreEditorial  = editorialLINQ.Nombre,
                                     IdIdioma = mediosLINQ.IdIdioma,
                                     NombreIdioma = idiomaLINQ.Nombre,
                                     IdAutor = mediosLINQ.IdAutor,
                                     NombreAutor = autorLINQ.Nombre,
                                     IdGenero = mediosLINQ.IdGenero,
                                     NombreGenero = generoLINQ.Nombre,
                                     Paginas = mediosLINQ.Paginas,
                                     Publicacion = mediosLINQ.Publicacion,
                                     CantidadEjemplares = mediosLINQ.CantidadEjemplares,
                                     CantidadEnPrestamo = mediosLINQ.CantidadEnPrestamo,
                                     Imagen = mediosLINQ.Imagen


                                 }).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Medio medio = new ML.Medio();
                                medio.IdMedio = item.IdMedio;
                                medio.Titulo = item.Titulo;

                                medio.TipoMedio = new ML.TipoMedio();
                                medio.TipoMedio.IdTipoMedio = item.IdTipoMedio.Value;
                                medio.TipoMedio.Nombre = item.NombreTipoMedio;

                                medio.Editorial = new ML.Editorial();
                                medio.Editorial.IdEditorial = item.IdEditorial.Value;
                                medio.Editorial.Nombre = item.NombreEditorial;

                                medio.Idioma = new ML.Idioma();
                                medio.Idioma.IdIdioma = item.IdIdioma.Value;
                                medio.Idioma.Nombre = item.NombreIdioma;


                                medio.Autor = new ML.Autor();
                                medio.Autor.IdAutor = item.IdAutor.Value;
                                medio.Autor.Nombre = item.NombreAutor;

                                medio.Genero = new ML.Genero();
                                medio.Genero.IdGenero = item.IdGenero.Value;
                                medio.Genero.Nombre = item.NombreGenero;

                                medio.Paginas = item.Paginas.Value;
                                medio.Publicacion = item.Publicacion.Value;
                                medio.CantidadEjemplares = item.CantidadEjemplares.Value;
                                medio.CantidadEnPrestamo = item.CantidadEnPrestamo.Value;
                                medio.Imagen = item.Imagen;

                                result.Objects.Add(medio);
                            }
                            result.Correct = true;
                            result.Message = "Medios consultados";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "La tabla de Medios esta vacia";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar los medios";
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
        public static ML.Result MedioGetById(int IdMedio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from mediosLINQ in context.Medios
                                 where mediosLINQ.IdMedio == IdMedio
                                 select new
                                 {
                                     IdMedio = mediosLINQ.IdMedio,
                                     Titulo = mediosLINQ.Titulo,
                                     IdTipoMedio = mediosLINQ.IdTipoMedio,
                                     IdEditorial = mediosLINQ.IdEditorial,
                                     IdIdioma = mediosLINQ.IdIdioma,
                                     IdAutor = mediosLINQ.IdAutor,
                                     IdGenero = mediosLINQ.IdGenero,
                                     Paginas = mediosLINQ.Paginas,
                                     Publicacion = mediosLINQ.Publicacion,
                                     CantidadEjemplares = mediosLINQ.CantidadEjemplares,
                                     CantidadEnPrestamo = mediosLINQ.CantidadEnPrestamo,
                                     Imagen = mediosLINQ.Imagen
                                 }
                                 ).FirstOrDefault();
                    if (query != null)
                    {
                        var item = query;
                        ML.Medio medio = new ML.Medio();
                        medio.IdMedio = item.IdMedio;
                        medio.Titulo = item.Titulo;

                        medio.TipoMedio = new ML.TipoMedio();
                        medio.TipoMedio.IdTipoMedio = item.IdTipoMedio.Value;

                        medio.Editorial = new ML.Editorial();
                        medio.Editorial.IdEditorial = item.IdEditorial.Value;

                        medio.Idioma = new ML.Idioma();
                        medio.Idioma.IdIdioma = item.IdIdioma.Value;

                        medio.Autor = new ML.Autor();
                        medio.Autor.IdAutor = item.IdAutor.Value;

                        medio.Genero = new ML.Genero();
                        medio.Genero.IdGenero = item.IdGenero.Value;

                        medio.Paginas = item.Paginas.Value;
                        medio.Publicacion = item.Publicacion.Value;
                        medio.PublicacionFormated = item.Publicacion.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        medio.CantidadEjemplares = item.CantidadEjemplares.Value;
                        medio.CantidadEnPrestamo = item.CantidadEnPrestamo.Value;
                        medio.Imagen = item.Imagen;

                        result.Object = medio;

                        result.Correct = true;
                        result.Message = "Medio consultado";

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo consultar el  medio";
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
