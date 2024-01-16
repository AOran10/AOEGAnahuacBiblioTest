using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class EditorialController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Form(int? IdEditorial)
        {
            ML.Editorial editorial = new ML.Editorial();

            if (IdEditorial == 0 || IdEditorial == null)
            {
                ViewBag.Accion = "Agregar Editorial";
            }
            else
            {
                ViewBag.Accion = "Actualizar Editorial";
                ML.Result result = await GetById(IdEditorial.Value);
                editorial = (ML.Editorial)result.Object;
            }
            return View(editorial);
        }

        public async Task<ML.Result> GetById(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/Editorial/getbyid/" + id);
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    

                    string objparticular = preresult.Object.ToString();

                    ML.Editorial resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.Editorial>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


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
        public IActionResult Form(ML.Editorial editorial, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();
            if ((editorial.Imagen != null && fuImagen != null) || (editorial.Imagen == null && fuImagen != null))
            {
                editorial.Imagen = ConvertirImagenABytes(fuImagen);
            }

            if (editorial.IdEditorial == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.Editorial.EditorialAdd(editorial);

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
                result = BL.Editorial.EditorialUpdate(editorial);

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
        public IActionResult Delete(int IdEditorial)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Editorial.EditorialDelete(IdEditorial);
            ViewBag.Mensaje = result.Message;
            return View("Modal");
        }
        public byte[] ConvertirImagenABytes(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        [HttpGet]
        public JsonResult GetAllEditorial()
        {
            ML.Result result = BL.Editorial.EditorialGetAll();
            return Json(result);
        }
    }
}
