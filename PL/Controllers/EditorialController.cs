using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EditorialController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form(int? IdEditorial)
        {
            ML.Editorial editorial = new ML.Editorial();

            if (IdEditorial == 0 || IdEditorial == null)
            {
                ViewBag.Accion = "Agregar Editorial";
            }
            else
            {
                ViewBag.Accion = "Actualizar Editorial";
                ML.Result result = BL.Editorial.EditorialGetById(IdEditorial.Value);
                editorial = (ML.Editorial)result.Object;
            }
            return View(editorial);
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
