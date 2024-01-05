using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace PL.Controllers
{
    public class PrestamoController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Prestamo.PrestamoGetAll();

            ML.Prestamo prestamo = new ML.Prestamo();
            prestamo.Prestamos = result.Objects;

            return View(prestamo);
        }

        [HttpGet]
        public IActionResult Form(int IdPrestamo)
        {
            ML.Prestamo prestamo = new ML.Prestamo();

            // ML.Result.resultUsers = BL.IdentityUser.GetAll();
            ML.Result resultMedio = BL.Medio.MedioGetAll();
            ML.Result resultStatus = BL.Status.StatusGetAll();

            prestamo.IdentityUsers = new ML.IdentityUser();
            prestamo.IdMedio = new ML.Medio();
            prestamo.IdStatus = new ML.Status();

            if (IdPrestamo == 0 || IdPrestamo == null)
            {
                ViewBag.Accion = "Registrar un Prestamo";
            }
            else
            {
                ViewBag.Accion = "Actualizar Prestamo";
                ML.Result result = BL.Prestamo.PrestamoGetById(IdPrestamo);
                prestamo = (ML.Prestamo)result.Object;
            }

            //prestamo.IdentityUser.IdentityUsers = resultIdentityUsers.Objects;
            prestamo.IdMedio.Medios = resultMedio.Objects;
            prestamo.IdStatus.StatusList = resultStatus.Objects;

            return View(prestamo);
        }

        [HttpPost]
        public IActionResult Form(ML.Prestamo prestamo)
        {
            ML.Result result = new ML.Result();

            if(prestamo.IdPrestamo == 0) 
            {
                ViewBag.Accion = "Agregar";
                result = BL.Prestamo.PrestamoAdd(prestamo);

                if (result.Correct)
                {
                    ViewBag.Messaje = result.Message;

                }
                else
                {
                    ViewBag.Messaje = "Ocurrió un error" + result.Message;
                }
                return View("Modal");
            }
            else
            {
                ViewBag.Accion = "Actualizar";
                result = BL.Prestamo.PrestamoUpdate(prestamo);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Ocurrió un Error al Actualizar" + result.Message;
                }
                return View("Modal");
            }
            
        }

        public IActionResult Delete(int IdPrestamo)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Prestamo.PrestamoDelete(IdPrestamo);
            ViewBag.Messaje = result.Message;
            return View("Modal");
        }

        [HttpGet]
        public JsonResult GetAllAutor()
        {
            ML.Result result = BL.Prestamo.PrestamoGetAll();
            return Json(result);
        }
    }
}
