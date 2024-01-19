using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class TipoMedioController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdTipoMedio)
        {
            ML.TipoMedio tipoMedio = new ML.TipoMedio();

            if (IdTipoMedio == 0 || IdTipoMedio == null)
            {
                ViewBag.Accion = "Agregar Tipo de medio";
            }
            else
            {
                ViewBag.Accion = "Actualizar Tipo de medio";
                ML.Result result = BL.TipoMedio.TipoMedioGetById(IdTipoMedio.Value);
                tipoMedio = (ML.TipoMedio)result.Object;
            }
            return View(tipoMedio);
        }

        // RestSharp 

        public async Task<ML.Result>GetById(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/TipoMedio/getbyid/" + id);
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    string objparticular = preresult.Object.ToString();

                    ML.TipoMedio resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.TipoMedio>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    result = preresult;
                    result.Object = resultobject;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        //

        [HttpPost]
        public IActionResult Form(ML.TipoMedio tipoMedio)
        {
            ML.Result result = new ML.Result();

            if (tipoMedio.IdTipoMedio == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.TipoMedio.TipoMedioAdd(tipoMedio);

                if (result.Correct)
                {
                    ViewBag.Mensaje = result.Message;
                }
                else
                {
                    ViewBag.Mensaje = "No se ingreso, ocurrio " + result.Message;
                }
                return View("Modal");
            }
            else
            {
                ViewBag.Accion = "Actualizar";
                result = BL.TipoMedio.TipoMedioUpdate(tipoMedio);

                if (result.Correct)
                {
                    ViewBag.Mensaje = result.Message;

                }
                else
                {
                    ViewBag.Mensaje = "No se actulizo, ocurrio " + result.Message;
                }
                return View("Modal");
            }
        }
        public IActionResult Delete(int IdTipoMedio)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.TipoMedio.TipoMedioDelete(IdTipoMedio);
            ViewBag.Mensaje = result.Message;
            return View("Modal");
        }
        public JsonResult GetAllTipoMedio()
        {
            ML.Result result = BL.TipoMedio.TipoMedioGetAll();
            return Json(result);
        }
    }
}
