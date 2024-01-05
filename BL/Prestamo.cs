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
            
            try 
            { 
                using(DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    SqlParameter IdentityUser = new SqlParameter("@IdentityUser", prestamo.IdentityUsers);
                    SqlParameter IdMedio = new SqlParameter("@IdMedio", prestamo.IdMedio);
                    SqlParameter FechaPrestamo = new SqlParameter("@FechaPrestamo", prestamo.FechaPrestamo);
                    SqlParameter FechaDevolucion = new SqlParameter("@FechaDevolucion", prestamo.FechaDevolucion);
                    SqlParameter IdStatus = new SqlParameter("@IdStatus", prestamo.IdStatus);

                    string store = "PrestamoAdd @IdentityUser, @IdMedio, @FechaPrestamo, @FechaDevolucion, @IdStatus";

                    var query = context.Database.ExecuteSqlRaw(store, IdentityUser, IdMedio, FechaPrestamo, FechaDevolucion, IdStatus);

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
                    SqlParameter IdentityUser = new SqlParameter("@IdentityUser", prestamo.IdentityUsers);
                    SqlParameter IdMedio = new SqlParameter("@IdMedio", prestamo.IdMedio);
                    SqlParameter FechaPrestamo = new SqlParameter("@FechaPrestamo", prestamo.FechaPrestamo);
                    SqlParameter FechaDevolucion = new SqlParameter("@FechaDevolucion", prestamo.FechaDevolucion);
                    SqlParameter IdStatus = new SqlParameter("@IdStatus", prestamo.IdStatus);

                    string store = "PrestamoUpdate @IdPrestamo, @IdentityUser, @IdMedio, @FechaPrestamo, @FechaDevolucion, @IdStatus";

                    var query = context.Database.ExecuteSqlRaw(store, IdPrestamo , IdMedio, FechaPrestamo, FechaDevolucion, IdStatus);

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

                    var query = context.Database.ExecuteSqlInterpolated($"PrestamoDelete {IdPrestamo}");

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

        public static ML.Result PrestamoGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AoeganahuacBiblioTestContext context= new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from prestamosLINQ in context.Prestamos
                                 select new
                                 {
                                     IdPrestamo = prestamosLINQ.IdPrestamo,
                                     IdentityUsers = prestamosLINQ.Id,
                                     IdMedio = prestamosLINQ.IdMedio,
                                     FechaPrestamo = prestamosLINQ.FechaPrestamo,   
                                     FechaDevolucion = prestamosLINQ.FechaDevolucion,
                                     IdStatus = prestamosLINQ.IdStatus
                                 }).ToList();

                    if(query != null)
                    {
                        if(query.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Prestamo prestamo = new ML.Prestamo();
                                prestamo.IdPrestamo = item.IdPrestamo;
                                prestamo.IdentityUsers = new ML.IdentityUser();
                                prestamo.IdentityUsers.IdUsuario = item.IdentityUsers;
                                prestamo.IdMedio = new ML.Medio();
                                prestamo.IdMedio.IdMedio = item.IdMedio.Value;
                                prestamo.FechaPrestamo = item.FechaPrestamo;
                                prestamo.FechaDevolucion = item.FechaDevolucion;
                                prestamo.IdStatus = new ML.Status();
                                prestamo.IdStatus.IdStatus = item.IdStatus.Value;

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
                                 where prestamosLINQ.IdPrestamo == IdPrestamo
                                 select new
                                 {
                                     IdPrestamo = prestamosLINQ.IdPrestamo,
                                     IdentityUsers = prestamosLINQ.Id,
                                     IdMedio = prestamosLINQ.IdMedio,
                                     FechaPrestamo = prestamosLINQ.FechaPrestamo,
                                     FechaDevolucion = prestamosLINQ.FechaDevolucion,
                                     IdStatus = prestamosLINQ.IdStatus
                                 }).FirstOrDefault();

                    if(query != null)
                    {
                        var item = query;
                        ML.Prestamo prestamo = new ML.Prestamo();
                        prestamo.IdPrestamo = item.IdPrestamo;
                        prestamo.IdentityUsers = new ML.IdentityUser();
                        prestamo.IdentityUsers.IdUsuario = item.IdentityUsers;
                        prestamo.IdMedio = new ML.Medio();
                        prestamo.IdMedio.IdMedio = item.IdMedio.Value;
                        prestamo.FechaPrestamo = item.FechaPrestamo;
                        prestamo.FechaDevolucion = item.FechaDevolucion;
                        prestamo.IdStatus = new ML.Status();
                        prestamo.IdStatus.IdStatus = item.IdStatus.Value;

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
