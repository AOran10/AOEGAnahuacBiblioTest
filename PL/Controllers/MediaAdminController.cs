using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MediaAdminController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Medio.MedioGetAll();

            ML.Medio medio = new ML.Medio();
            medio.Medios = result.Objects;

            return View(medio);
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
        [HttpPost]
        public IActionResult Form(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

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
    }
}
