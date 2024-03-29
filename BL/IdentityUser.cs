﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class IdentityUser
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = (from user in context.AspNetUsers
                                 select new
                                 {
                                     IdUser = user.Id,
                                     UserName = user.UserName,
                                     Email = user.Email,  // PRUEB JWT
                                     PasswordHash = user.PasswordHash  // PRUEBA JWT

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.IdentityUser usuario = new ML.IdentityUser();
                            usuario.IdUsuario = item.IdUser;
                            usuario.UserName = item.UserName;
                            usuario.Email = item.Email;          //JWT 
                            usuario.Password = item.PasswordHash;   //JWT
                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;

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

        public static ML.Result Asignar(ML.IdentityUser user)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AoeganahuacBiblioTestContext context = new DL.AoeganahuacBiblioTestContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.Rol.RoleId}'");

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrió un Error al Asignar el Rol, intentalo más tarde";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;   // Error 
                result.Ex = ex;
            }

            return result;
        }
    }
}