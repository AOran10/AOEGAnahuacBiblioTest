using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdAutor)
        {
            ML.Autor autor = new ML.Autor();

            if (IdAutor == 0 || IdAutor == null)
            {
                ViewBag.Accion = "Agregar Autor";
            }
            else
            {
                ViewBag.Accion = "Actualizar Autor";
                ML.Result result = BL.Autor.AutorGetById(IdAutor.Value);
                autor = (ML.Autor)result.Object;
            }
            return View(autor);
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
