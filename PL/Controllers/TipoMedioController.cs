using Microsoft.AspNetCore.Mvc;

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
