using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class IdiomaController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdIdioma)
        {
            ML.Idioma idioma = new ML.Idioma();

            if (IdIdioma == 0 || IdIdioma == null)
            {
                ViewBag.Accion = "Agregar Idioma";
            }
            else
            {
                ViewBag.Accion = "Actualizar Idioma";
                ML.Result result = BL.Idioma.IdiomaGetById(IdIdioma.Value);
                idioma = (ML.Idioma)result.Object;
            }
            return View(idioma);
        }

        // RestSharp Idiomas

        public async Task<ML.Result> GetById(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/Idioma/getbyid/" + id);
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    string objparticular = preresult.Object.ToString();

                    ML.Idioma resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.Idioma>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
        public IActionResult Form(ML.Idioma idioma)
        {
            ML.Result result = new ML.Result();

            if (idioma.IdIdioma == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.Idioma.IdiomaAdd(idioma);

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
                result = BL.Idioma.IdiomaUpdate(idioma);

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
        public IActionResult Delete(int IdIdioma)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Idioma.IdiomaDelete(IdIdioma);
            ViewBag.Mensaje = result.Message;
            return View("Modal");
        }
        [HttpGet]
        public JsonResult GetAllIdioma()
        {
            ML.Result result = BL.Idioma.IdiomaGetAll();
            return Json(result);
        }
    }
}
