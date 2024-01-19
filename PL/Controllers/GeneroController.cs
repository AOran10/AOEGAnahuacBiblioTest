using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class GeneroController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdGenero)
        {
            ML.Genero genero = new ML.Genero();

            if (IdGenero == 0 || IdGenero == null)
            {
                ViewBag.Accion = "Agregar Genero";
            }
            else
            {
                ViewBag.Accion = "Actualizar Genero";
                ML.Result result = BL.Genero.GeneroGetById(IdGenero.Value);
                genero = (ML.Genero)result.Object;
            }
            return View(genero);
        }

        // RestSharp

        public async Task<ML.Result> GetById(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/Genero/getbyid/" + id);    // Obtiene Información de la petición
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)                    // Deserialización
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    string objparticular = preresult.Object.ToString();

                    ML.Genero resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.Genero>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
        public IActionResult Form(ML.Genero genero)
        {
           ML.Result result = new ML.Result();

            if (genero.IdGenero == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.Genero.GeneroAdd(genero);

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
                result = BL.Genero.GeneroUpdate(genero);

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
        public IActionResult Delete(int IdGenero)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Genero.GeneroDelete(IdGenero);
            ViewBag.Mensaje = result.Message;
            return View("Modal");
        }
        public JsonResult GetAllGenero()
        {
            ML.Result result = BL.Genero.GeneroGetAll();
            return Json(result);
        }
    }
}
