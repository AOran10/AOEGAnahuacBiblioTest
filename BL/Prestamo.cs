using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Prestamo
    {
        public static ML.Result PrestamoAdd(ML.Prestamo prestamo)
        {
            ML.Result result = new ML.Result();

            prestamo.FechaPrestamo = DateTime.Now;

            try 
            { 
                using(DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter IdUsuario = new SqlParameter("@IdUsuario", prestamo.IdentityUsers.IdUsuario);
                    SqlParameter IdMedio = new SqlParameter("@IdMedio", prestamo.Medio.IdMedio);
                    SqlParameter FechaPrestamo = new SqlParameter("@FechaPrestamo", prestamo.FechaPrestamo);
                    SqlParameter FechaDevolucion = new SqlParameter("@FechaDevolucion", prestamo.FechaDevolucion);

                    string store = "PrestamoAdd @IdUsuario, @IdMedio, @FechaPrestamo, @FechaDevolucion";

                    var query = context.Database.ExecuteSqlRaw(store, IdUsuario, IdMedio, FechaPrestamo, FechaDevolucion);


                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha creado el Nuevo Registro de Prestamo Correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Algo salió mal. Intentalo más tarde";
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

        public static ML.Result PrestamoUpdate(ML.Prestamo prestamo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter IdPrestamo = new SqlParameter("@IdPrestamo", prestamo.IdPrestamo);
                    SqlParameter IdUsuario = new SqlParameter("@IdUsuario", prestamo.IdentityUsers.IdUsuario);
                    SqlParameter IdMedio = new SqlParameter("@IdMedio", prestamo.Medio.IdMedio);
                    SqlParameter FechaPrestamo = new SqlParameter("@FechaPrestamo", prestamo.FechaPrestamo);
                    SqlParameter FechaDevolucion = new SqlParameter("@FechaDevolucion", prestamo.FechaDevolucion);
                    SqlParameter IdEstatusPrestamo = new SqlParameter("@IdEstatus", prestamo.EstatusPrestamo.IdEstatusPrestamo);

                    string store = "PrestamoUpdate @IdPrestamo, @IdUsuario, @IdMedio, @FechaPrestamo, @FechaDevolucion, @IdEstatus";

                    var query = context.Database.ExecuteSqlRaw(store, IdPrestamo , IdUsuario, IdMedio, FechaPrestamo, FechaDevolucion, IdEstatusPrestamo);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "La Actualizacion fue hecha con Exito";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "La Actualización no pudo ser Completada";
                    }
                }
            }
            catch (Exception ex) 
            { 
                result.Correct = false;
                result.Message= ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result PrestamoDelete(int IdPrestamo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter idPrestamo = new SqlParameter("@IdPrestamo", IdPrestamo);

                    var query = context.Database.ExecuteSqlInterpolated($"PrestamoTerminar {IdPrestamo}");

                    if( query > 0 )
                    {
                        result.Correct = true;
                        result.Message = "Se Eliminó el Registro";
                    }
                    else
                    {
                        result.Correct= false;
                        result.Message = "El registro NO pudo ser Eliminado";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result PrestamoGetAll(int filtro)
        {
            ML.Result result = new ML.Result();
			
            try
            {
                using(DL.AoeganahuacBiblioTestContext context= new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from prestamosLINQ in context.Prestamos
                                 join usuarioLINQ in context.AspNetUsers on prestamosLINQ.IdUsuario equals usuarioLINQ.Id
                                 join medioLINQ in context.Medios on prestamosLINQ.IdMedio equals medioLINQ.IdMedio
                                 join estatusLINQ in context.EstatusPrestamos on prestamosLINQ.IdEstatus equals estatusLINQ.IdEstatusPrestamo
                                 where estatusLINQ.IdEstatusPrestamo == filtro

								 select new
                                 {
                                     IdPrestamo = prestamosLINQ.IdPrestamo,
                                     IdUsuario = prestamosLINQ.IdUsuario,
                                     UserName = usuarioLINQ.UserName,
                                     IdMedio = prestamosLINQ.IdMedio,
                                     Titulo = medioLINQ.Titulo,
                                     FechaPrestamo = prestamosLINQ.FechaPrestamo,
                                     FechaDevolucion = prestamosLINQ.FechaDevolucion,
                                     IdEstatus = prestamosLINQ.IdEstatus,
                                     Descripcion = estatusLINQ.Descripcion
                                 }).ToList();

                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Prestamo prestamo = new ML.Prestamo();
                                prestamo.IdPrestamo = item.IdPrestamo;
                                prestamo.IdentityUsers = new ML.IdentityUser();
                                prestamo.IdentityUsers.IdUsuario = item.IdUsuario;
                                prestamo.IdentityUsers.UserName = item.UserName;
                                prestamo.Medio = new ML.Medio();
                                prestamo.Medio.IdMedio = item.IdMedio.Value;
                                prestamo.Medio.Titulo = item.Titulo;
                                prestamo.FechaPrestamo = item.FechaPrestamo;
                                prestamo.FechaDevolucion = item.FechaDevolucion;
                                prestamo.EstatusPrestamo = new ML.EstatusPrestamo();
                                prestamo.EstatusPrestamo.IdEstatusPrestamo = item.IdEstatus.Value;
                                prestamo.EstatusPrestamo.Descripcion = item.Descripcion;

                                result.Objects.Add(prestamo);
                            }

                            result.Correct = true;
                            result.Message = "Todos los Prestamos";
                        }
                        else
                        {
                            result.Correct = true;
                            result.Message = "No se encontrarón Registros";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se pudo completar la Consulta";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result PrestamoGetById(int IdPrestamo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from prestamosLINQ in context.Prestamos
                                 join usuarioLINQ in context.AspNetUsers on prestamosLINQ.IdUsuario equals usuarioLINQ.Id
                                 join medioLINQ in context.Medios on prestamosLINQ.IdMedio equals medioLINQ.IdMedio
                                 join estatusLINQ in context.EstatusPrestamos on prestamosLINQ.IdEstatus equals estatusLINQ.IdEstatusPrestamo
                                 where prestamosLINQ.IdPrestamo == IdPrestamo
                                 select new
                                 {
                                     IdPrestamo = prestamosLINQ.IdPrestamo,
                                     IdUsuario = prestamosLINQ.IdUsuario,
                                     UserName = usuarioLINQ.UserName,
                                     IdMedio = prestamosLINQ.IdMedio,
                                     Titulo = medioLINQ.Titulo,
                                     FechaPrestamo = prestamosLINQ.FechaPrestamo,
                                     FechaDevolucion = prestamosLINQ.FechaDevolucion,
                                     IdEstatus = prestamosLINQ.IdEstatus,
                                     Descripcion = estatusLINQ.Descripcion
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        var item = query;
                        ML.Prestamo prestamo = new ML.Prestamo();
                        prestamo.IdPrestamo = item.IdPrestamo;
                        prestamo.IdentityUsers = new ML.IdentityUser();
                        prestamo.IdentityUsers.IdUsuario = item.IdUsuario;
                        prestamo.IdentityUsers.UserName = item.UserName;
                        prestamo.Medio = new ML.Medio();
                        prestamo.Medio.IdMedio = item.IdMedio.Value;
                        prestamo.FechaPrestamo = item.FechaPrestamo;
                        prestamo.FechaDevolucion = item.FechaDevolucion;
                        prestamo.EstatusPrestamo = new ML.EstatusPrestamo();
                        prestamo.EstatusPrestamo.IdEstatusPrestamo = item.IdEstatus.Value;
                        prestamo.EstatusPrestamo.Descripcion = item.Descripcion;

                        result.Object = prestamo;
                        result.Correct = true;
                        result.Message = "Consulta Compleatada";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "La Consulta NO pudo ser Completada";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;    
                result.Ex = ex;
            }

            return result;
        }
    }
}
