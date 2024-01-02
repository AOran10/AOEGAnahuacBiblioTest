using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class GeneroController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Genero.GeneroGetAll();
            ML.Genero genero = new ML.Genero();
            genero.Generos = result.Objects;
            return View(genero);
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
        
    }
}
