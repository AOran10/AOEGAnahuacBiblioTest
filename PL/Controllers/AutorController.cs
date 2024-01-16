using Microsoft.AspNetCore.Mvc;
using ML;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>  Form(int? IdAutor)
        {
            ML.Autor autor = new ML.Autor();

            if (IdAutor == 0 || IdAutor == null)
            {
                ViewBag.Accion = "Agregar Autor";
            }
            else
            {
                ViewBag.Accion = "Actualizar Autor";
                ML.Result result = await GetById(IdAutor.Value);
                autor = (ML.Autor)result.Object;
            }
            return View(autor);
        }


        public async Task<ML.Result> GetById(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/Autor/getbyid/" + id);
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ML.Result preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                   

                    string objparticular = preresult.Object.ToString();

                    ML.Autor resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.Autor>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    

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
        public IActionResult Form(ML.Autor autor, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();
            if ((autor.Imagen != null && fuImagen != null) || (autor.Imagen == null && fuImagen != null))
            {
                autor.Imagen = ConvertirImagenABytes(fuImagen);
            }

            if (autor.IdAutor == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.Autor.AutorAdd(autor);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha ingresado correctamente el autor";
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
                result = BL.Autor.AutorUpdate(autor);

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
        public IActionResult Delete(int IdAutor)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Autor.AutorDelete(IdAutor);
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
        public JsonResult GetAllAutor()
        {
            ML.Result result = BL.Autor.AutorGetAll();
            return Json(result);
        }
    }
}
