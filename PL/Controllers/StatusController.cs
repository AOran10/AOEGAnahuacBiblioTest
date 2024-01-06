using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Status.StatusGetAll();
            ML.Status status= new ML.Status();
            status.StatusList = result.Objects;
            return View(status);
        }

        // FORM
    }
}
