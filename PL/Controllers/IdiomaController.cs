using Microsoft.AspNetCore.Mvc;

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
