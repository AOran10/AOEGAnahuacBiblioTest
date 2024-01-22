using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class PrestamoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }
    [HttpGet]
    public JsonResult GetAllPrestamo(int filtro)
        {
            ML.Result result = BL.Prestamo.PrestamoGetAll(filtro);


            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? IdPrestamo)
        {
            ML.Prestamo prestamo = new ML.Prestamo();

            ML.Result resultUsers = BL.IdentityUser.GetAll();
            ML.Result resultMedio = BL.Medio.MedioGetAll();
            ML.Result resultStatus = BL.Status.StatusGetAll();

            prestamo.IdentityUsers = new ML.IdentityUser();
            prestamo.Medio = new ML.Medio();
            prestamo.EstatusPrestamo = new ML.EstatusPrestamo();

            if (IdPrestamo == 0 || IdPrestamo == null)
            {
                ViewBag.Accion = "Registrar un Prestamo";
            }
            else
            {
                ViewBag.Accion = "Actualizar Prestamo";
                ML.Result result = await GetById(IdPrestamo.Value);
                prestamo = (ML.Prestamo)result.Object;
            }

            //prestamo.IdentityUser.IdentityUsers = resultIdentityUsers.Objects;
            prestamo.Medio.Medios = resultMedio.Objects;
            prestamo.EstatusPrestamo.EstatusPrestamoList = resultStatus.Objects;
            prestamo.IdentityUsers.IdentityUsers = resultUsers.Objects;

			return View(prestamo);
        }

        public async Task<ML.Result> GetById(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/Prestamo/getbyid/" + id);
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                    string objparticular = preresult.Object.ToString();

                    ML.Prestamo resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.Prestamo>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                    result = preresult;
                    result.Object = resultobject;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
                //throw;
            }
            return result;
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

        
    }
}
