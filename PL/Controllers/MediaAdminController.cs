using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace PL.Controllers
{
    public class MediaAdminController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdMedio)
        {
            ML.Medio medio = new ML.Medio();

            ML.Result resultTipoMedios = BL.TipoMedio.TipoMedioGetAll();
            ML.Result resultEditorial = BL.Editorial.EditorialGetAll();
            ML.Result resultIdioma = BL.Idioma.IdiomaGetAll();
            ML.Result resultAutor = BL.Autor.AutorGetAll();
            ML.Result resultGenero = BL.Genero.GeneroGetAll();

            medio.TipoMedio = new ML.TipoMedio();
            medio.Editorial = new ML.Editorial();
            medio.Idioma = new ML.Idioma();
            medio.Autor = new ML.Autor();
            medio.Genero = new ML.Genero();

            if (IdMedio == 0 || IdMedio == null)
            {
                ViewBag.Accion = "Agregar nuevo medio";
            }
            else
            {
                ViewBag.Accion = "Actualizar medio";
                ML.Result result = BL.Medio.MedioGetById(IdMedio.Value);
                medio = (ML.Medio)result.Object;
            }

            medio.TipoMedio.TipoMedios = resultTipoMedios.Objects;
            medio.Editorial.Editoriales = resultEditorial.Objects;
            medio.Idioma.Idiomas = resultIdioma.Objects;
            medio.Autor.Autores = resultAutor.Objects;
            medio.Genero.Generos = resultGenero.Objects;

            return View(medio);
        }

        // RestSharp MediaAdmin

        public async Task<ML.Result>GetById(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                var options = new RestClientOptions("http://localhost:5056/api/TipoMedio/getbyid/" + id);
                var Client = new RestClient(options);
                var request = new RestRequest("");
                var response = await Client.GetAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    ML.Result  preresult = System.Text.Json.JsonSerializer.Deserialize<ML.Result>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    string objparticular = preresult.Object.ToString();

                    ML.TipoMedio resultobject = System.Text.Json.JsonSerializer.Deserialize<ML.TipoMedio>(objparticular, new JsonSerializerOptions { PropertyNameCaseInsensitive =true });

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
        public IActionResult Form(ML.Medio medio, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();

            if ((medio.Imagen != null && fuImagen != null) || (medio.Imagen == null && fuImagen != null))
            {
                medio.Imagen = ConvertirImagenABytes(fuImagen);
            }

            if (medio.IdMedio == 0)
            {
                ViewBag.Accion = "Añadir";
                result = BL.Medio.MedioAdd(medio);

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
                result = BL.Medio.MedioUpdate(medio);

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
        public IActionResult Delete(int IdMedio)
        {
            ViewBag.Accion = "Eliminar";
            ML.Result result = BL.Medio.MedioDelete(IdMedio);
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
        public JsonResult GetAllMedia()
        {
            ML.Result result = BL.Medio.MedioGetAll();

            
            return Json(result);
        }
    }
}
